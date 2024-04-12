using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.EventSystems;

public class KennyController : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private DoorController _dc;

    private float _moveSpeed = 2.5f;
    private float _rotateSpeed = 10f;
    
    public bool IsWalking { get; set; }

    private Rigidbody _rb;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontalMovement, 0f, verticalMovement).normalized;

        _rb.velocity = movement * _moveSpeed;

        if (movement.magnitude > 0.1f)
        {
            IsWalking = true;
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, Quaternion.LookRotation(movement), _rotateSpeed * Time.fixedDeltaTime));
        }
        else
        {
            IsWalking = false;
        }

        _anim.SetBool("IsWalking", IsWalking);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("DoorTrigger") && Input.GetKeyDown(KeyCode.F) && !IsWalking)
        {
            _anim.SetTrigger("DoorOpen");
            _dc.OpenDoor();
        }
    }
}
