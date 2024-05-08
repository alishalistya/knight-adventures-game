using System.Collections;
using UnityEngine;

public class OrbQuestChicken : BaseOrb
{
    public override string OrbName { get; } = "QuestChicken";
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger");
            QuestEvents.OrbCollected(OrbName, 1);
            gameObject.SetActive(false);
        }
    }

}