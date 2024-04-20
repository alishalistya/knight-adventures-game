using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    Rigidbody playerRigidbody;
    int floorMask;
    Animator anim;                      // Reference to the animator component.
    float camRayLength = 100f;
    bool isGrounded = true;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }

    void FixedUpdate()
    {


        MoveAndTurn();
        Animating();
    }

    void MovePlayer(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     if (isGrounded)
        //     {
        //         GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
        //     }
        // }

    }

    void MoveAndTurn()
    {
        float v = Input.GetAxisRaw("Vertical");
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            movement = playerToMouse.normalized * Math.Abs(v) * speed * Time.deltaTime;
            // if v is negative, move backwards
            if (v < 0)
            {
                movement = -movement;
            }
            playerRigidbody.MovePosition(transform.position + movement);

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating()
    {
        bool walking = movement.magnitude > 0;
        // check if walking backwards
        Vector3 copyMovement = movement;
        copyMovement.y = 0;
        bool walkingBackwards = walking && Vector3.Dot(copyMovement, transform.forward) < 0;
        anim.SetBool("IsWalkingBackward", walkingBackwards);
        anim.SetBool("IsWalking", walking && !walkingBackwards);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
