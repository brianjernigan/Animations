using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator _anim;
    
    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }
    
    public void OpenDoor()
    {
        _anim.SetTrigger("OpenDoor");
    }
}
