using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public void LoadQuest(Quest quest)
    {
        foreach (Goal goal in quest.Goals)
        {
            Debug.Log("Test");
            UpdateQuestUI(goal.Description, goal.CurrentAmount + "/" + goal.RequiredAmount);
        }
    }

    public void UpdateQuestUI(string questName, string description)
    {
        Debug.Log("Quest Name: " + questName + " Description: " + description);
    }
}