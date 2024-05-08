using System.Collections;
using UnityEngine;

public class OrbLocationArea : BaseOrb
{
    public override string OrbName { get; } = "LocationArea";
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            QuestEvents.OrbCollected(OrbName, 1);
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        {
            QuestEvents.OrbCollected(OrbName, -1);
        }
    }
}