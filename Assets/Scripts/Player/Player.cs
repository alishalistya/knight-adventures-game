using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Entity, IShopCustomer
{
    [SerializeField] PlayerMovement movement;
    [SerializeField] GameObject handslot;
    [SerializeField] RangedWeapon defaultWeapon;
    [SerializeField] MeleeWeapon meleeWeapon;
    [SerializeField] RangedWeapon thirdWeapon;

    [SerializeField] GameObject UIGameOver;

    [SerializeField] private GameObject[] PetPrefabs;
    [SerializeField] private GameObject PowerOrbPrefabs;

    public GameManager gameManager;

    protected override float _damageMultiplier
    {
        get
        {
            if (PlayerCheats.IsCheat(StatusCheats.ONE_HIT_KILL))
            {
                return 10000f; // multiply damage by 10000
            }

            return 1f + gameManager.buffDamageTaken * 0.1f;
        }
    }

    float _attackSpeed = 1;
    float AttackSpeed
    {
        get { return _attackSpeed; }
        set
        {
            _attackSpeed = value;
        }
    }

    bool _isAttacking = false;

    public PlayerMovement Movement => movement;
    override protected bool IsAttacking
    {
        get { return _isAttacking; }
        set
        {
            if (!value)
            {
                Inventory.CurrentWeapon.IsActive = false;
            }
            _isAttacking = value; movement.disableMove = value;
            Inventory.IsChangeEnabled = !value;
        }
    }

    public PlayerInventory Inventory;
    public int Gold { get; set; } = 0;

    protected int baseHealth = 200;
    protected int initialMaxHealth;
    protected int initialInitialHealth;

    protected override int MaxHealth => initialMaxHealth;
    protected override int InitialHealth => initialInitialHealth;

    protected float _playerDifficultyMultiplier;
    private bool _disableAction = true;
    private bool DisableAction
    {
        get => _disableAction; set { _disableAction = value; movement.disableMove = value; Inventory.IsChangeEnabled = !value; }
    }

    private void Awake()
    {
        PersistanceManager.Instance.AssertInit();
        gameManager = GameManager.Instance;
        _playerDifficultyMultiplier = gameManager.Difficulty switch
        {
            Difficulty.Easy => 1f,
            Difficulty.Medium => 0.8f,
            Difficulty.Hard => 0.6f,
            _ => 1f
        };
        initialMaxHealth = (int)(_playerDifficultyMultiplier * baseHealth);

        if (gameManager.FromLoad)
        {
            Debug.Log("Saved Health: " + GameManager.Instance.PlayerHealth);
            initialInitialHealth = GameManager.Instance.PlayerHealth;
            transform.SetPositionAndRotation(new Vector3(gameManager.PlayerPosition[0], gameManager.PlayerPosition[1], gameManager.PlayerPosition[2]), Quaternion.Euler(gameManager.PlayerRotation[0], gameManager.PlayerRotation[1], gameManager.PlayerRotation[2]));
        }
        else
        {
            initialInitialHealth = (int)(_playerDifficultyMultiplier * baseHealth);
        }

        Gold = GameManager.Instance.PlayerGold;

        // Debug.Log("Initial Player Health: " + initialInitialHealth);

        if (gameManager.HasKnight)
        {
            BuyItem(ShopItem.ShopItemType.Pet_1);
        }

        if (gameManager.HasMage)
        {
            BuyItem(ShopItem.ShopItemType.Pet_2);
        }
    }

    new void Start()
    {
        base.Start();
        QuestEvents.OnQuestCompleted += AddGoldFromQuest;
        Inventory = new PlayerInventory(handslot, defaultWeapon, meleeWeapon, thirdWeapon);
        OnGameStateChange(GameManager.Instance.GameState);

        initialInitialHealth = GameManager.Instance.PlayerHealth;

        PlayerStatsEvents.PlayerStatsChanged(this);
    }

    public void ChangeWeapon()
    {
        Inventory.ChangeNextWeapon();
    }

    void Update()
    {
        if (IsDead)
        {
            return;
        }

        Inventory?.Update();

        // get mouse is down not currently clicked
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (DisableAction || IsAttacking || movement.PlayerMovementState == PlayerMovementState.Jumping)
        {
            return;
        }
        movement.TurnToCamera();
        BaseWeapon weapon = Inventory.CurrentWeapon;
        movement.Anim.SetFloat("AttackSpeed", AttackSpeed * weapon.AttackSpeedMultiplier);
        weapon.AnimateAttack(movement.Anim);
        if (weapon is RangedWeapon)
        {
            PersistanceManager.Instance.GlobalStat.AddTotalShot();
            GameManager.Instance.Statistics.AddTotalShot();
        }
    }

    protected override void OnDeath()
    {
        movement.enabled = false;
        movement.Anim.SetTrigger("Death");
        UIGameOver.SetActive(true);
        PersistanceManager.Instance.GlobalStat.AddDeath();
        GameManager.Instance.Statistics.AddDeath();
    }

    protected override void OnDamaged(int prevHealth, int currentHealth)
    {
        PlayerStatsEvents.PlayerStatsChanged(this);
        // print($"Damaged, current health {currentHealth}");
    }

    public void BuyItem(ShopItem.ShopItemType item)
    {
        ShopItem.BuyItem(item);
        Instantiate(PetPrefabs[(int)item], transform.position, Quaternion.identity);
    }

    public override void TakeDamage(int amount)
    {
        if (PlayerCheats.IsCheat(StatusCheats.NO_DAMAGE))
        {
            return;
        }
        base.TakeDamage(amount);
        PersistanceManager.Instance.GlobalStat.AddDamageTaken(amount);
        GameManager.Instance.Statistics.AddDamageTaken(amount);
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        PlayerStatsEvents.PlayerStatsChanged(this);
    }

    public void AddGoldFromQuest(Quest quest)
    {
        Gold += quest.GoldReward;
        Debug.Log($"Quest completed, gold reward: {quest.GoldReward}");
        PlayerStatsEvents.PlayerStatsChanged(this);
    }

    public void RemoveGold(int amount)
    {
        if (PlayerCheats.IsCheat(StatusCheats.MOTHERLODE))
        {
            PlayerStatsEvents.PlayerStatsChanged(this);
            return;
        }
        Gold -= amount;
        PlayerStatsEvents.PlayerStatsChanged(this);
    }

    public bool CheckGold(int cost)
    {
        if (PlayerCheats.IsCheat(StatusCheats.MOTHERLODE))
        {
            return true;
        }
        return Gold >= cost;
    }

    public void KillAllMobPets()
    {
        var pets = FindObjectsOfType<BasePetMob>();
        foreach (var pet in pets)
        {
            pet.TakeDamage(pet.Health.CurrentHealth.value);
        }
    }

    public void CheatNextQuest()
    {
        var quest = FindObjectOfType<Quest>();
        if (quest != null)
        {
            quest.CheatQuest();
        }
    }

    public void CheatGivePowerOrb()
    {
        float distanceInFront = 2f;
        Vector3 positionInFront = transform.position + transform.forward * distanceInFront;
        Instantiate(PowerOrbPrefabs, positionInFront, Quaternion.identity);
    }

    private void OnDestroy()
    {
        QuestEvents.OnQuestCompleted -= AddGoldFromQuest;

        if (Health.CurrentHealth.value <= 0)
        {
            gameManager.PlayerHealth = initialMaxHealth;
        }
        else
        {
            gameManager.PlayerHealth = Health.CurrentHealth.value;
        }

        gameManager.PlayerGold = Gold;

        PlayerStatsEvents.PlayerStatsChanged(this);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChange += OnGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChange -= OnGameStateChange;
    }

    private void OnGameStateChange(GameState state)
    {
        Debug.Log("STATE CHANGE: " + state);
        if (state != GameState.PLAYING)
        {
            DisableAction = true;
        }
        else
        {
            DisableAction = false;
        }
    }
}
