using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerAim aim;
    [SerializeField] GameObject handslot;
    PlayerInventory inventory;

    private void Awake()
    {
        inventory = new PlayerInventory(handslot);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
