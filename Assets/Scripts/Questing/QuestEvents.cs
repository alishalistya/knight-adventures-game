using UnityEngine;

public class QuestEvents : MonoBehaviour
{
    public delegate void CollectEventHandler(string orbName, int amount);
    public static event CollectEventHandler OnItemCollected;

    public static void OrbCollected(string orbName, int amount = 1)
    {
        OnItemCollected?.Invoke(orbName, amount);
    }

    public delegate void GoalEventHandler(Quest quest);
    public static event GoalEventHandler OnGoalProgressed;
    public static void GoalProgressed(Quest quest)
    {
        OnGoalProgressed?.Invoke(quest);
    }

    public static event QuestEventHandler OnQuestCompleted;
    public delegate void QuestEventHandler(Quest quest);
    public static void QuestCompleted(Quest quest)
    {
        OnQuestCompleted?.Invoke(quest);
    }

}