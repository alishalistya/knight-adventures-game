
public class KillGoal : Goal 
{
    public int MobID {get; set;}
    public KillGoal(Quest quest, int mobID, string description, bool completed, int currentAmount, int requiredAmount) 
    {
        this.Quest = quest;
        this.MobID = mobID;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        CombatEvents.OnMobKilled += MobKilled;
    }

    private void MobKilled(UnityEngine.Vector3 position, Mob mob)
    {
        if (mob.ID == this.MobID)
        {
            this.CurrentAmount++;
            Evaluate();
        }
    }
}