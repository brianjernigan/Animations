using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KennyController : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
}
