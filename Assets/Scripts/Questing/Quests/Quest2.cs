using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest2 : Quest
{
    public void Awake()
    {
        QuestName = "Kill all the kerocos";
        Description = "Destroy the kerocos to save the town";
        GoldReward = 20;
        Goals.Add(new KillGoal(this, 0, "Kill 5 kerocos", false, 0, 5));
        Goals.Add(new AreaGoal(this, "LocationArea", "Stand in front of the door", false, 0, 1));
        Goals.ForEach(g => g.Init());
    }

    protected override void GiveReward()
    {
        Debug.Log("Quest2 Completed");
        SceneManager.LoadScene("Quest-3");
    }
}