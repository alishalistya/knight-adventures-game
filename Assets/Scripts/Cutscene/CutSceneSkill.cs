using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneSkill : MonoBehaviour
{
    PlayableDirector director;
    [SerializeField] GameObject UI;

    void Start()
    {
        director = GetComponentInChildren<PlayableDirector>();
    }

    public void PlayCutscene()
    {
        // GameManager.Instance.GameState = GameState.CUTSCENE;
        director.Play();
    }

}
