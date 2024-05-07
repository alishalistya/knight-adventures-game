using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{

    private void OnEnable()
    {
        var buttons = new List<GameObject>();
        foreach (Transform child in transform)
        {
            buttons.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

        PersistanceManager.Instance.LoadSaveDescriptions();
        // TODO: remove this, only debug
        PersistanceManager.Instance.SaveDescriptions.SetDummyData();
        print("Descriptions: " + PersistanceManager.Instance.SaveDescriptions.Descriptions.Count);
        for (int i = 0; i < PersistanceManager.Instance.SaveDescriptions.Descriptions.Count && i < 3; i++)
        {
            var description = PersistanceManager.Instance.SaveDescriptions.Descriptions[i];
            buttons[i].SetActive(true);
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = description.ToString();
            var button = buttons[i].GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                // TODO: Load game
                print("Load game: ");
            });
        }
    }
}
