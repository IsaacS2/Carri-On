using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class VultureAnimator : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }
}
