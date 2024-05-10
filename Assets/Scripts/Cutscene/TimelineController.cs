using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    PlayableDirector director;
    [SerializeField] GameObject cutscene;
    [SerializeField] GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponentInChildren<PlayableDirector>();
        UI.SetActive(false);
        GameManager.Instance.GameState = GameState.CUTSCENE;
        if (GameManager.Instance.FromLoad)
        {
            director.Stop();
            UI.SetActive(true);
            cutscene.SetActive(false);
            GameManager.Instance.GameState = GameState.PLAYING;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (director.state != PlayState.Playing)
        {
            ShopEvents.ShopTimerStarted();
            director.Stop();
            UI.SetActive(true);
            cutscene.SetActive(false);
            if (GameManager.Instance.GameState == GameState.CUTSCENE)
            {
                GameManager.Instance.GameState = GameState.PLAYING;
            }
        }
    }
}
