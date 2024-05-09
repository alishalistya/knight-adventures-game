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
    }

    // Update is called once per frame
    void Update()
    {
        if(director.state != PlayState.Playing)
        {
            director.Stop();
            UI.SetActive(true);
            cutscene.SetActive(false);
            
        }
    }
}
