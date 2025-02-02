using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SafeHouse : MonoBehaviour
{
    [SerializeField] private GameObject SafeHouseUI;

    private bool IsQuestCompleted = true;
    private bool IsSaveButtonSet = false;
    private void Awake()
    {
        QuestEvents.OnQuestCompleted += OnQuestCompleted;
        TextMeshProUGUI informationText = SafeHouseUI.transform.Find("informationText").GetComponent<TextMeshProUGUI>();
        informationText.text = "Click The save button you want to save your progress";
        SafeHouseUI.SetActive(false);
        PersistanceManager.Instance.LoadSaveDescriptions();
    }

    private void OnQuestCompleted(Quest quest)
    {
        IsQuestCompleted = true;
        GameManager.Instance.GoalProgress = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }

    private void SetSaveButton(Player player)
    {
        Transform safeHouseUITransform = SafeHouseUI.transform;
        SaveDescriptions.Description Description1 = PersistanceManager.Instance.SaveDescriptions.Descriptions[0];
        SaveDescriptions.Description Description2 = PersistanceManager.Instance.SaveDescriptions.Descriptions[1];
        SaveDescriptions.Description Description3 = PersistanceManager.Instance.SaveDescriptions.Descriptions[2];


        Button saveButton1 = safeHouseUITransform.Find("button1").GetComponent<Button>();
        TextMeshProUGUI descText1 = saveButton1.transform.Find("descText").GetComponent<TextMeshProUGUI>();


        if (Description1 != null && Description1.IsUsed)
        {

            descText1.SetText(Description1.ToString());
        }
        else
        {
            descText1.SetText("Save 1 is empty");
        }

        Button saveButton2 = safeHouseUITransform.Find("button2").GetComponent<Button>();
        TextMeshProUGUI descText2 = saveButton2.transform.Find("descText").GetComponent<TextMeshProUGUI>();
        if (Description2 != null && Description2.IsUsed)
        {
            descText2.SetText(Description2.ToString());
        }
        else
        {
            descText2.SetText("Save 2 is empty");
        }
        Button saveButton3 = safeHouseUITransform.Find("button3").GetComponent<Button>();
        TextMeshProUGUI descText3 = saveButton3.transform.Find("descText").GetComponent<TextMeshProUGUI>();
        if (Description3 != null && Description3.IsUsed)
        {
            descText3.SetText(Description3.ToString());
        }
        else
        {
            descText3.SetText("Save 3 is empty");
        }

        saveButton1.onClick.RemoveAllListeners();

        saveButton1.onClick.AddListener(() =>
        {
            SaveGame(player, 1);
        });

        saveButton2.onClick.RemoveAllListeners();

        saveButton2.onClick.AddListener(() =>
        {
            SaveGame(player, 2);
        });

        saveButton3.onClick.RemoveAllListeners();
        saveButton3.onClick.AddListener(() =>
        {
            SaveGame(player, 3);
        });
        IsSaveButtonSet = true;
    }


    public void OnTriggerStay(Collider other)
    {
        var mob = FindObjectOfType<Mob>();
        if (other.CompareTag("Player") && (mob == null))
        {
            var player = other.GetComponent<Player>();
            if (!IsSaveButtonSet)
            {
                SetSaveButton(player);
            }
            SafeHouseUI.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SafeHouseUI.SetActive(false);
        }
    }

    public void SaveGame(Player player, int saveIndex)
    {
        TMP_InputField userInputField = SafeHouseUI.transform.Find("saveInput").GetComponent<TMP_InputField>();
        var pets = GameObject.FindGameObjectsWithTag("Player's Pet");

        bool hasKnight = false;
        bool hasMage = false;

        foreach (var pet in pets)
        {
            var petEntity = pet.GetComponent<Entity>();

            if (petEntity is PetAttacker)
            {
                hasKnight = true;
            }
            else if (petEntity is PetHealer)
            {
                hasMage = true;
            }
        }

        var saveData = new SaveData
        (
            GameManager.Instance.Statistics,
            player.Health.CurrentHealth.value,
            player.Gold,
            GameManager.Instance.QuestNumber,
            player.transform.position,
            player.transform.rotation.eulerAngles,
            player.Inventory.CurrentWeaponIndex,
            PlayerCheats._instance._statusCheats,
            GameManager.Instance.Difficulty,
            GameManager.Instance.buffDamageTaken,
            GameManager.Instance.IsAyamAlive,
            GameManager.Instance.GoalProgress,
            hasKnight,
            hasMage
        );

        var SaveDescription = new SaveDescriptions.Description(userInputField.text + " - Save " + saveIndex);
        GameManager.SaveGame(saveData, SaveDescription, saveIndex - 1);
        SetSaveButton(player);
    }

    private void OnDestroy()
    {
        QuestEvents.OnQuestCompleted -= OnQuestCompleted;
    }
}