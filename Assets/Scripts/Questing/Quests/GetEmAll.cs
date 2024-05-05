using UnityEngine;

public class GetEmAll : Quest
{
    public void Awake()
    {
        Debug.Log("GetEmAll Quest Started");
        QuestName = "Get 'Em All";
        Description = "Kill all the Kerocos";
        Goals.Add(new CollectGoal(this, "DamageBuff", "Collect 5 Orb Potion", false, 0, 5));
        Goals.ForEach(g => g.Init());
    }
}