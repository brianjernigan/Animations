using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class KennyController : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody _rb;
    
    [SerializeField] private DoorController _dc;
    [SerializeField] private GameObject _doorTrigger;
    [SerializeField] private GameObject _goldSphere;
    
    private float _moveSpeed = 2.5f;
    private float _rotateSpeed = 10f;

    private bool IsWalking { get; set; }
    private bool IsSneaking { get; set; }
    private bool CanOpenDoor { get; set; }
    
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !IsWalking)
        {
            Dance();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !IsWalking)
        {
            Backflip();
        }

        if (Input.GetKey(KeyCode.LeftShift) && IsWalking)
        {
            IsSneaking = true;
        }
        else
        {
            IsSneaking = false;
        }

        if (CanOpenDoor && Input.GetKeyDown(KeyCode.F) && !IsWalking)
        {
            _anim.SetTrigger("DoorOpen");
            _dc.OpenDoor();
            _doorTrigger.SetActive(false);
            CanOpenDoor = false;
        }
        
        HandleMovingAnims();
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
    }

    private void HandleMovingAnims()
    {
        _anim.SetBool("IsWalking", IsWalking);
        _anim.SetBool("IsSneaking", IsSneaking);
    }

    private void Dance()
    {
        _anim.SetTrigger("ChickenDance");
    }

    private void Backflip()
    {
        _anim.SetTrigger("Backflip");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DoorTrigger"))
        {
            CanOpenDoor = true;
        }

        if (other.gameObject.CompareTag("GoldSphere"))
        {
            _anim.SetTrigger("Breakdance");
            StartCoroutine(DeactivateSphere());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DoorTrigger"))
        {
            CanOpenDoor = false;
        }
    }

    private IEnumerator DeactivateSphere()
    {
        _goldSphere.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        _goldSphere.SetActive(true);
    }
}
