using System.Collections;
using UnityEngine;

abstract public class BaseOrb : MonoBehaviour
{
    abstract public string OrbName { get;}
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger");
            QuestEvents.OrbCollected(OrbName);
            gameObject.SetActive(false);
            OrbEffects(other.GetComponent<Player>());
        }
    }

   abstract protected void OrbEffects(Player player);
   void StartOrbTimer()
    {
         StartCoroutine(OrbTimer());
    }

    private IEnumerator OrbTimer()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }

    void Start()
    {
        StartOrbTimer();
    }
}