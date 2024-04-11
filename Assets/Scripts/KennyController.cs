using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.EventSystems;

public class KennyController : MonoBehaviour
{
    private Animator _anim;

    private float _moveSpeed = 5f;
    private float _rotateSpeed = 10f;
    
    public bool IsWalking { get; set; }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontalMovement, 0f, verticalMovement).normalized;

        if (movement.magnitude > Mathf.Epsilon)
        {
            IsWalking = true;
            transform.Translate(movement * (_moveSpeed * Time.deltaTime), Space.World);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), _rotateSpeed * Time.deltaTime);
        }
        else
        {
            IsWalking = false;
        }

        _anim.SetBool("IsWalking", IsWalking);
    }
}
