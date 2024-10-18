using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureAnimator : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    void Awake()
    {
        if (!_anim)
        {
            Destroy(this);
        }
    }

    public void SetBoolean(string _boolName, bool _boolValue)
    {
        _anim.SetBool(_boolName, _boolValue);
    }

    public void SetTrig(string _triggerName)
    {
        _anim.SetTrigger(_triggerName);
    }

    public void ResetTrig(string _triggerName)
    {
        _anim.ResetTrigger(_triggerName);
    }
}
