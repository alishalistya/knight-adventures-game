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
        // todo update here to update initial health (examnple: case to load health from save game)
        initialInitialHealth = (int)(_playerDifficultyMultiplier * baseHealth);
    }

    new void Start()
    {
        base.Start();
        QuestEvents.OnQuestCompleted += AddGoldFromQuest;
        Inventory = new PlayerInventory(handslot, defaultWeapon, meleeWeapon, thirdWeapon);
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
        PersistanceManager.Instance.GlobalStat.AddPlayTime(Time.deltaTime);
        GameManager.Instance.Statistics.AddPlayTime(Time.deltaTime);
    }

    public void Attack()
    {
        if (IsAttacking || movement.PlayerMovementState == PlayerMovementState.Jumping)
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
        // insert code for play sound effect, update ui here
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
        // QuestEvents.QuestCompleted();
    }
    private void OnDestroy() {
        QuestEvents.OnQuestCompleted -= AddGoldFromQuest;
    }
}
