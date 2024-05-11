using System;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {
    public int QuestInWhatScene { get; set; }
    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int GoldReward { get; set; }

    public void CheckGoals() 
    {
        Completed = Goals.TrueForAll(g => g.Completed);
        if (Completed) 
        {
            QuestEvents.QuestCompleted(this);
            GiveReward();
        }
    }

    protected virtual void GiveReward()
    {
        Debug.Log("Quest Completed");
        
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

        GameManager.Instance.HasKnight = hasKnight;
        GameManager.Instance.HasMage = hasMage;

        GameManager.Instance.GoalProgress = new int[10];
    }
    
    private void OnDestroy() {
        Goals.ForEach(g => g.Cleanup());
    }
    public void CheatQuest()
    {
        Completed = true;
        QuestEvents.QuestCompleted(this);
        GiveReward();
    }

    public void LoadQuestFromSave(int[] goalProgress)
    {
        for (int i = 0; i < Goals.Count; i++)
        {
            Goals[i].CurrentAmount = goalProgress[i];
            Goals[i].Evaluate();
        }
    }

    public void SaveQuestToGoalProgress(int[] goalProgress)
    {
        for (int i = 0; i < Goals.Count; i++)
        {
            goalProgress[i] = Goals[i].CurrentAmount;
        }
    }
}