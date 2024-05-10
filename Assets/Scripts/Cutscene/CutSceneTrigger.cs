using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour
{
    PlayableDirector director;
    GameObject cutscene;

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponentInChildren<PlayableDirector>();
        cutscene = GameObject.Find("Ending Cutscene");
        cutscene.SetActive(false);

        // if (cutscene != null)
        // {
        //     cutscene.SetActive(true); // Ensure the GameObject is active
        //     Debug.Log("Cutscene activated at start.");
        // }
        // else
        // {
        //     Debug.Log("Failed to find the cutscene GameObject.");
        // }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            cutscene.SetActive(true);
            Debug.Log("P key was pressed.");
            if (cutscene.activeSelf)
            {
                Debug.Log("Cutscene is active.");
            }
            else
            {
                Debug.Log("Cutscene is inactive.");
            }

            director.Play();
        }

        if (director.state != PlayState.Playing)
        {
            director.Stop();
            cutscene.SetActive(false);
        }
    }

}
