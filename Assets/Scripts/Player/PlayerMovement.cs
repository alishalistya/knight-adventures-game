using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            if (value == PlayerMovementState.Running && !footstepSound.isPlaying)
            {
                footstepSound.Play();
            }
            else if (value != PlayerMovementState.Running && footstepSound.isPlaying)
            {
                footstepSound.Stop();
            }
            Anim.SetInteger("MovementState", (int)_playerMovementState);
        }
    }

    protected HashSet<MovementSpeedMultiplierBuff> _movementSpeedMultiplierBuffs = new();
    public HashSet<MovementSpeedMultiplierBuff> MovementSpeedMultiplierBuffs => _movementSpeedMultiplierBuffs;

    float MoveSpeed
    {
        get
        {
            var baseSpeed = PlayerCheats.IsCheat(StatusCheats.TWO_TIMES_SPEED) ? 20 : 10;

            return (1f + _movementSpeedMultiplierBuffs.Sum(mul => mul.Value)) * baseSpeed;
        }
    }

    [HideInInspector] public Vector3 dir;
    float hzInput, vInput;

    public bool disableMove = false;
    [SerializeField] float groundYOffset;
    Rigidbody rb;
    [SerializeField] Transform cam;
    [SerializeField] AudioSource footstepSound;

    float turnSmoothVelocity;
    float turnSmoothTime = 0.1f;

    [SerializeField] public Animator Anim;

    private Vector3 previousPosition;

    private void Start()
    {
        previousPosition = transform.position;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PlayerMovementState = PlayerMovementState.Idle;
    }

    private void FixedUpdate()
    {
        GetDirectionAndMove();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, previousPosition);
        PersistanceManager.Instance.GlobalStat.AddDistance(distance);
        GameManager.Instance.Statistics.AddDistance(distance);
        previousPosition = transform.position;
        // CheckJump();
        Animating();
    }

    void GetDirectionAndMove()
    {
        if (disableMove)
        {
            rb.velocity = Vector3.zero;
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
            dir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * MoveSpeed;
            rb.velocity = new Vector3(dir.x, rb.velocity.y, dir.z);
            // }
            // else
            // {
            //     dir = Vector3.zero;
            // }
        }
        else
        {
            dir = Vector3.zero;
            rb.velocity = Vector3.zero;
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
        // if (!IsGrounded())
        // {
        //     PlayerMovementState = PlayerMovementState.Jumping;
        //     return;
        // }

        if (!isMoving || !IsGrounded())
        {
            PlayerMovementState = PlayerMovementState.Idle;
            return;
        }

        PlayerMovementState = PlayerMovementState.Running;
    }
}

