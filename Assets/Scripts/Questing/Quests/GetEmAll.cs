using UnityEngine;

public class GetEmAll : Quest
{
    public void Awake()
    {
        QuestName = "Get 'Em All";
        Description = "Kill all the Kerocos";
        GoldReward = 20;
        Goals.Add(new CollectGoal(this, "BuffDamage", "Collect 5 Orb Potion", false, 0, 5));
        Goals.Add(new KillGoal(this, 0, "Kill 5 Kerocos", false, 0, 5));
        Goals.Add(new KillGoal(this, 1, "Kill 5 Kerocossssss", false, 0, 5));
        Goals.ForEach(g => g.Init());
    }
}