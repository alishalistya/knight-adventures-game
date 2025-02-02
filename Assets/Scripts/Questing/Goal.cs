using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    public Quest Quest { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }

    public virtual void Init() {}
    public virtual void Cleanup() {}

    public void Evaluate()
    {
        GameManager.Instance.GoalProgress = Quest.SaveQuestToGoalProgress();
        if (CurrentAmount >= RequiredAmount)
        {
            Complete();
        }
        else 
        {
            Completed = false;
        }
        Debug.Log("Goal Progress: " + GameManager.Instance.GoalProgress);
        QuestEvents.GoalProgressed(Quest);
    }

    public void Complete()
    {
        Completed = true;
        Quest.CheckGoals();
    }


}
