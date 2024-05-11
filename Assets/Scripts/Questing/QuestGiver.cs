using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private QuestUI questUI;
    public bool AssignedQuest { get; set; }
    [SerializeField] private GameObject quests;
    [SerializeField] private string questType;
    private Quest Quest { get; set; }
 

    private void Awake() {
        if (GameManager.Instance.FromLoad)
        {
            Debug.Log("Loading Quest Awake");
            Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
            Quest.LoadQuestFromSave(GameManager.Instance.GoalProgress);
        }
        else
        {
            Debug.Log("No Quest to load Awake");
            Quest = null;
        }
    }
    void Start()
    {
        AssignQuest();
    }

    void AssignQuest()
    {
        AssignedQuest = true;
        Debug.Log("Assigning Quest");
        if (Quest != null)
        {
            Debug.Log("Quest already exists");
        } 
        else
        {
            Debug.Log("Creating Quest");
            Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
        }
        questUI.LoadQuest(Quest);
    }

}