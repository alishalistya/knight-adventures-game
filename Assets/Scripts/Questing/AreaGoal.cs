public class AreaGoal : Goal 
{
    public string AreaName {get; set;}
    public AreaGoal(Quest quest, string areaName, string description, bool completed, int currentAmount, int requiredAmount) 
    {
        this.Quest = quest;
        this.AreaName = areaName;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        QuestEvents.OnItemCollected += AreaDetected;
    }
    
    void AreaDetected(string areaName, int amount) {
        if (areaName == this.AreaName)
        {
            this.CurrentAmount += amount;
            Evaluate();
        }
    }
}