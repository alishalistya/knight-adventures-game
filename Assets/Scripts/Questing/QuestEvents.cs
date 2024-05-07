using UnityEngine;

public class QuestEvents : MonoBehaviour
{
    public delegate void CollectEventHandler(string orbName);
    public static event CollectEventHandler OnItemCollected;

    public static void OrbCollected(string orbName)
    {
        OnItemCollected?.Invoke(orbName);
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