using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest3 : Quest
{
    public void Awake()
    {
        QuestName = "Keroco Conquest";
        Description = "Help the Blue Town!";
        GoldReward = 40;

        Goals.Add(new KillGoal(this, 0, "Kill 5 kerocos", false, 0, 5));

        Goals.Add(new KillGoal(this, 1, "Kill 2 Kepala Keroco", false, 0, 2));

        Goals.Add(new KillGoal(this, 2, "Kill 1 Jendral", false, 0, 1));

        Goals.Add(new AreaGoal(this, "LocationArea", "Find the Mage!", false, 0, 1));


        Goals.ForEach(g => g.Init());
    }

    protected override void GiveReward()
    {
        Debug.Log("Quest1 Completed");
        SceneManager.LoadScene("Quest-2");
    }
}