using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitSoundPlayer))]
public class Bed : MonoBehaviour
{
    public AudioClip bedShakeClip;
    public AudioClip growlClip;

    private UnitSoundPlayer _soundPlayer;
    private Animator _animator;

    private Action onComplete;

    void Awake()
    {
        _soundPlayer = GetComponent<UnitSoundPlayer>();
        _animator = GetComponent<Animator>();
    }

    public void BedShakeAnimation(Action callback)
    {
        onComplete = callback;
        _animator.SetTrigger("BedShake");
    }

    public void BedShakeSound()
    {
        _soundPlayer.PlayOneShot(bedShakeClip, 0.3f);
    }

    public void GrowlSound()
    {
        _soundPlayer.PlayOneShot(growlClip);
    }

    public void Complete()
    {
        onComplete();
    }
}
