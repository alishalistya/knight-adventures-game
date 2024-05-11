using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest4 : Quest
{
    public void Awake()
    {
        QuestInWhatScene = 4;
        QuestName = "Dead King's Rise";
        Description = "Defeat the King.";
        GoldReward = 100;

        Goals.Add(new KillGoal(this, 0, "Kill 4 kerocos", false, 0, 4));

        Goals.Add(new KillGoal(this, 1, "Kill 2 Kepala Keroco", false, 0, 2));

        Goals.Add(new KillGoal(this, 2, "Kill 2 Jendral", false, 0, 2));

        Goals.Add(new KillGoal(this, 3, "Kill the King", false, 0, 1));


        Goals.ForEach(g => g.Init());
    }

    protected override void GiveReward()
    {
        base.GiveReward();
        Debug.Log("Quest4 Completed");

    }
}