using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodStainEvent : AbstractEvent
{
    public AudioClip bloodSplatterClip;

    private Animator _animator;

    protected override void ChildAwake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Enter()
    {
        _soundPlayer.PlayOneShot(bloodSplatterClip);
        _animator.SetTrigger("Enter");
    }

    public override void Exit()
    {
        onExit();
    }
}
