using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest3 : Quest
{
    public void Awake()
    {
        QuestInWhatScene = 3;
        QuestName = "Keroco Conquest";
        Description = "Help the Blue Town!";
        GoldReward = 40;

        Goals.Add(new KillGoal(this, 0, "Kill 5 kerocos", false, 0, 5));

        Goals.Add(new KillGoal(this, 1, "Kill 2 Kepala Keroco", false, 0, 2));

        Goals.Add(new KillGoal(this, 2, "Kill 1 Jendral", false, 0, 1));

        Goals.Add(new AreaGoal(this, "LocationArea", "Find the Mage!", false, 0, 1));

        Goals.ForEach(g => g.Init());
    }

    private void Start()
    {
        if (GameManager.Instance.FromLoad)
        {
            foreach (var mob in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(mob);
            }
        }
    }

    protected override void GiveReward()
    {
        base.GiveReward();
        GameManager.NextQuest();
    }
}