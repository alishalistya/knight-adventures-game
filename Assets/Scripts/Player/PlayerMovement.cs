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
    Rigidbody rb;
    [SerializeField] Transform cam;

    float turnSmoothVelocity;
    float turnSmoothTime = 0.1f;

    [SerializeField] public Animator Anim;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Anim.SetInteger("MovementState", (int)MovementState.Idle);
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

        dir = new Vector3(hzInput, 0f, vInput);
        dir.Normalize();

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            dir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * (isWalking ? moveSpeed : runningSpeed);
            rb.MovePosition(transform.position + dir * Time.deltaTime);
        }
        else
        {
            dir = Vector3.zero;
        }
    }

    void CheckJump()
    {
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
            Anim.SetInteger("MovementState", (int)MovementState.Jumping);
            return;
        }

        if (!isMoving || !IsGrounded())
        {
            Anim.SetInteger("MovementState", (int)MovementState.Idle);
            return;
        }
        if (isWalking)
        {
            Anim.SetInteger("MovementState", (int)MovementState.Walking);
        }
        else
        {
            Anim.SetInteger("MovementState", (int)MovementState.Running);
        }
    }
}

