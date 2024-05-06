using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerMovementState
{
    Idle,
    Running,
    Jumping
}

public class PlayerMovement : MonoBehaviour
{
    PlayerMovementState _playerMovementState;
    public PlayerMovementState PlayerMovementState
    {
        get { return _playerMovementState; }
        set
        {
            _playerMovementState = value;
            Anim.SetInteger("MovementState", (int)_playerMovementState);
        }
    }

    float moveSpeed = 20f;
    [HideInInspector] public Vector3 dir;
    float hzInput, vInput;

    public bool disableMove = false;
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
        PlayerMovementState = PlayerMovementState.Idle;
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
        if (disableMove)
        {
            return;
        }
        vInput = Input.GetAxisRaw("Vertical");
        hzInput = Input.GetAxisRaw("Horizontal");

        dir = new Vector3(hzInput, 0f, vInput);
        dir.Normalize();

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // if (!disableMove)
            // {
            dir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * moveSpeed;
            rb.MovePosition(transform.position + dir * Time.deltaTime);
            // }
            // else
            // {
            //     dir = Vector3.zero;
            // }
        }
        else
        {
            dir = Vector3.zero;
        }
    }

    public void TurnToCamera()
    {
        transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);
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
            PlayerMovementState = PlayerMovementState.Jumping;
            return;
        }

        if (!isMoving || !IsGrounded())
        {
            PlayerMovementState = PlayerMovementState.Idle;
            return;
        }

        PlayerMovementState = PlayerMovementState.Running;
    }
}

