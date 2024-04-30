using UnityEngine;

abstract public class BaseOrb : MonoBehaviour
{
   void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger");

            gameObject.SetActive(false);
            OrbEffects(other.GetComponent<Player>());
        }
   }

   abstract protected void OrbEffects(Player player);
}