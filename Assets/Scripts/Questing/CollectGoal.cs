public class CollectGoal : Goal 
{
    public string ItemName {get; set;}
    public CollectGoal(Quest quest, string itemStringName, string description, bool completed, int currentAmount, int requiredAmount) 
    {
        this.Quest = quest;
        this.ItemName = itemStringName;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
    }
    
    void ItemCollected(string itemName) {
        if (itemName == this.ItemName)
        {
            this.CurrentAmount++;
            Evaluate();
        }
    }
}