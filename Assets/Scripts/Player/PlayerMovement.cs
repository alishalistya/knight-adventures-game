using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum MovementState
{
    Idle,
    Walking,
    Running,
    Jumping
}

public class PlayerMovement : MonoBehaviour
{
    float moveSpeed = 10f;
    float runningSpeed = 20f;
    [HideInInspector] public Vector3 dir;
    float hzInput, vInput;
    bool isWalking = false;

    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;
    Rigidbody rb;
    BoxCollider boxCollider;
    [SerializeField] Transform camFollowPos;

    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        anim.SetInteger("MovementState", (int)MovementState.Idle);
    }
    void Start()
    {
    }

    void Update()
    {
        GetDirectionAndMove();
        CheckJump();
        Animating();
    }

    void GetDirectionAndMove()
    {
        vInput = Input.GetAxisRaw("Vertical");
        hzInput = Input.GetAxisRaw("Horizontal");
        isWalking = Input.GetKey(KeyCode.LeftShift);

        dir = transform.forward * vInput + transform.right * hzInput;
        dir.Normalize();
        dir = (isWalking ? moveSpeed : runningSpeed) * dir.normalized;
        rb.MovePosition(transform.position + dir * Time.deltaTime);
    }

    void CheckJump()
    {
        Debug.Log(rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        // rb.velocity.y near 0
        return rb.velocity.y < 0.01f && rb.velocity.y > -0.01f;
    }


    void Animating()
    {
        bool isMoving = dir.magnitude > 0;
        if (!IsGrounded())
        {
            anim.SetInteger("MovementState", (int)MovementState.Jumping);
            return;
        }

        if (!isMoving || !IsGrounded())
        {
            anim.SetInteger("MovementState", (int)MovementState.Idle);
            return;
        }
        if (isWalking)
        {
            anim.SetInteger("MovementState", (int)MovementState.Walking);
        }
        else
        {
            anim.SetInteger("MovementState", (int)MovementState.Running);
        }
    }
}

