using System;
using TMPro;
using UnityEngine;
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
    }

    private void OnQuestCompleted(Quest quest)
    {
        IsQuestCompleted = true;
    }

    private void setSaveButton(Player player)
    {
        Transform safeHouseUITransform = SafeHouseUI.transform;
        Button saveButton1 = safeHouseUITransform.Find("button1").GetComponent<Button>();
        Button saveButton2 = safeHouseUITransform.Find("button2").GetComponent<Button>();
        Button saveButton3 = safeHouseUITransform.Find("button3").GetComponent<Button>();

        saveButton1.onClick.AddListener(() =>
        {
            SaveGame(player, 1);
        });

        saveButton2.onClick.AddListener(() =>
        {
            SaveGame(player, 2);
        });

        saveButton3.onClick.AddListener(() =>
        {
            SaveGame(player, 3);
        });
        IsSaveButtonSet = true;
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && IsQuestCompleted)
        {
            var player = other.GetComponent<Player>();
            if (!IsSaveButtonSet)
            {
                setSaveButton(player);
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
        PersistanceManager.Instance.LoadSaveDescriptions();
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
            GameManager.Instance.CurrentQuest
        );
        DateTime now = DateTime.Now;
        DateTime utcNow = now.ToUniversalTime();
        long unixTimestamp = (long)(utcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        var SaveDescription = new SaveDescriptions.Description
        {
            Name = "Save " + saveIndex,
            Time = unixTimestamp,
        };
        GameManager.SaveGame(saveData, SaveDescription, saveIndex-1);
    }
}