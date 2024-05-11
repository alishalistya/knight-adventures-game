using System.Collections;
using UnityEngine;

public class OrbQuestChicken : BaseOrb
{
    public int ChikenIndex;
    public override string OrbName { get; } = "QuestChicken";
    private void Start() {
        if (GameManager.Instance.IsAyamAlive[ChikenIndex] == false)
        {
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger");
            GameManager.Instance.IsAyamAlive[ChikenIndex] = false;
            QuestEvents.OrbCollected(OrbName, 1);
            gameObject.SetActive(false);
        }
    }

}