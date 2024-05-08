using System.Collections;
using UnityEngine;

abstract public class BuffOrb : BaseOrb
{
    public int BuffOrbTimer = 5;

    void StartOrbTimer()
    {
         StartCoroutine(OrbTimer());
    }

    private IEnumerator OrbTimer()
    {
        yield return new WaitForSeconds(BuffOrbTimer);
        gameObject.SetActive(false);
    }

    void Start()
    {
        StartOrbTimer();
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            QuestEvents.OrbCollected(OrbName);
            gameObject.SetActive(false);
            OrbEffects(other.GetComponent<Player>());
        }
    }

   abstract protected void OrbEffects(Player player);
}