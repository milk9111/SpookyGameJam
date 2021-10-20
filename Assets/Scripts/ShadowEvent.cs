using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEvent : AbstractEvent
{
    public List<AudioClip> growlClips;

    private Animator _animator;

    public override void Enter()
    {
        _animator.SetTrigger("Enter");
    }

    public void GrowlSound()
    {
        _soundPlayer.PlayOneShot(growlClips[Random.Range(0, growlClips.Count)]);
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
