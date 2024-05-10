using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the inspector

    void OnEnable()
    {
        // Instantly snap to the player's position when the object is enabled
        transform.position = player.position;
    }
}