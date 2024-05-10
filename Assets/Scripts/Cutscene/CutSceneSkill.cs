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
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemyObject in enemies)
        {
        Mob enemy = enemyObject.GetComponent<Mob>();
        if (enemy != null && enemy.movement != null)
        {
            enemy.TakeDamage(1000000);
        }
    }
        director.Play();
    }

}
