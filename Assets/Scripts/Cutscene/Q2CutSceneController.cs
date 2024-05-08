using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Q2CutSceneController : MonoBehaviour
{
    PlayableDirector director;
    GameObject cutscene;
    [SerializeField] GameObject healthUI;

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponentInChildren<PlayableDirector>();
        cutscene = GameObject.Find("Opening Cutscene 2");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(director.state != PlayState.Playing)
        {
            director.Stop();
            healthUI.SetActive(true);
            cutscene.SetActive(false);
            
        }
    }
}
