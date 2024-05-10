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


    private void Awake() {
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
            // SceneManager.LoadScene("Main Menu");
            cutscene.SetActive(false);

        }
    }

    public void PlayCutscene(Quest quest)
    {

        Debug.Log("PlayCutscene called.");
        if (quest.QuestName == "Dead King's Rise")
        {
            cutscene.SetActive(true);
            director.Play();
            UI.SetActive(false);
            GameManager.Instance.GameState = GameState.CUTSCENE;
            // if (GameManager.Instance.FromLoad)
            // {
            //     director.Stop();
            //     UI.SetActive(true);
            //     cutscene.SetActive(false);
            //     GameManager.Instance.GameState = GameState.PLAYING;
            // }

        }
            // cutscene.SetActive(true);
            // director.Play();
    }

    private void OnDestroy()
    {
        QuestEvents.OnQuestCompleted -= PlayCutscene;
    }

}
