using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXEvent : AbstractEvent
{
    public AudioClip clip;

    [Range(0.01f, 1f)]
    public float volume = 1f;

    public float timeTillExitSeconds;

    private bool _started;
    private float _endTime;

    private void Update()
    {
        if (!_started)
        {
            return;
        }

        if (Time.unscaledTime >= _endTime)
        {
            Exit();
        }
    }

    public override void Enter()
    {
        _started = true;
        _endTime = Time.unscaledTime + timeTillExitSeconds;
        _soundPlayer.PlayOneShot(clip, volume);
    }

    public override void Exit()
    {
        _started = false;
        onExit();
    }

    protected override void ChildAwake()
    {
        
    }
}
