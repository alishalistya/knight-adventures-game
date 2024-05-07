using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    private Transform container;
    private Transform goalItemTemplate;
    private void Awake()
    {
        container = transform.Find("Container");
        goalItemTemplate = container.Find("GoalItemTemplate");
        QuestEvents.OnGoalProgressed += UpdateQuestUI;
        goalItemTemplate.gameObject.SetActive(false);
    }
    public void LoadQuest(Quest quest)
    {
        gameObject.SetActive(true);

        var questNameText = gameObject.transform.Find("questName").GetComponent<TMPro.TextMeshProUGUI>();
        questNameText.SetText(quest.QuestName);

        var questDescriptionText = gameObject.transform.Find("questDescription").GetComponent<TMPro.TextMeshProUGUI>();
        questDescriptionText.SetText(quest.Description);

        int positionIndex = 0;
        float goalItemHeight = 40f;

        foreach (Goal goal in quest.Goals)
        {
            Debug.Log("Loading Goal");
            Transform goalItemTransform = Instantiate(goalItemTemplate, container);
            goalItemTransform.name = "GoalItem" + (positionIndex + 1);
            string goalProgress = goal.Description + " " + goal.CurrentAmount + "/" + goal.RequiredAmount;
            goalItemTransform.Find("goalProgress").GetComponent<TMPro.TextMeshProUGUI>().SetText(goalProgress);

            goalItemTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -goalItemHeight * positionIndex);
            goalItemTransform.gameObject.SetActive(true);

            positionIndex++;
        }
    }

    public void UpdateQuestUI(Quest quest)
    {
        foreach (Transform child in container)
        {
            if (child == goalItemTemplate) continue;
            Destroy(child.gameObject);
        }
        Debug.Log("Updating Quest UI");
        LoadQuest(quest);
    }
}