using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveSpeed = 6f;
    [HideInInspector] public Vector3 dir;
    float hzInput, vInput;

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
    }
    void Start()
    {
    }

    void Update()
    {
        GetDirectionAndMove();
        isGrounded();
        Animating();
    }

    void GetDirectionAndMove()
    {
        vInput = Input.GetAxisRaw("Vertical");
        hzInput = Input.GetAxisRaw("Horizontal");
        dir = transform.forward * vInput + transform.right * hzInput;
        dir.Normalize();
        dir = moveSpeed * Time.deltaTime * dir.normalized;
        rb.MovePosition(transform.position + dir);
    }

    bool isGrounded()
    {
        return true;
    }


    void Animating()
    {
        bool walking = dir.magnitude > 0;
        anim.SetBool("IsWalking", walking);
    }
}
