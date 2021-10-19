using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEvent : AbstractEvent
{
    private Animator _animator;

    public override void Enter()
    {
        _animator.SetTrigger("Enter");
    }

    public override void Exit()
    {
        onExit();
    }

    protected override void ChildAwake()
    {
        _animator = GetComponent<Animator>();
    }
}
