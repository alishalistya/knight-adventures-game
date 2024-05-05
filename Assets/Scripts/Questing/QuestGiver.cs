using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private QuestUI questUI;
    public bool AssignedQuest { get; set; }
    [SerializeField] private GameObject quests;
    [SerializeField] private string questType;
    private Quest Quest { get; set; }

    void Start()
    {
        AssignQuest();
    }

    void AssignQuest()
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
        questUI.LoadQuest(Quest);
    }

}