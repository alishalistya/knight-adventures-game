using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutSceneTrigger : MonoBehaviour
{
    PlayableDirector director;
    GameObject cutscene;
    [SerializeField] GameObject UI;
    private Quest quest;


    private void Awake()
    {
        QuestEvents.OnQuestCompleted += PlayCutscene;
    }

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponentInChildren<PlayableDirector>();
        cutscene = GameObject.Find("Ending Cutscene");
        cutscene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (director.state != PlayState.Playing)
        {
            director.Stop();
            if ((quest != null) && quest.Completed)
            {
                Debug.Log("Quest completed. Loading main menu..");
                GameManager.MainMenu();
            }
            cutscene.SetActive(false);

        }
    }

    public void PlayCutscene(Quest quest)
    {

        Debug.Log("PlayCutscene called.");
        if (quest.QuestName == "Dead King's Rise")
        {
            Debug.Log(UI);
            GameManager.Instance.GameState = GameState.CUTSCENE;

            UI.SetActive(false);
            Debug.Log("Cutscene triggered.");
            this.quest = quest;

            cutscene.SetActive(true);
            director.Play();

            GameManager.Instance.GameState = GameState.CUTSCENE;
            if (GameManager.Instance.FromLoad)
            {
                director.Stop();
                UI.SetActive(true);
                cutscene.SetActive(false);
                GameManager.Instance.GameState = GameState.PLAYING;
            }

        }
    }

    private void OnDestroy()
    {
        QuestEvents.OnQuestCompleted -= PlayCutscene;
    }

}
