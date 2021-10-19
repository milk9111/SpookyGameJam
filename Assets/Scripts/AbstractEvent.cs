using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitSoundPlayer))]
public abstract class AbstractEvent : MonoBehaviour
{
    protected UnitSoundPlayer _soundPlayer;

    public Action onExit;

    private void Awake()
    {
        _soundPlayer = GetComponent<UnitSoundPlayer>();

        ChildAwake();
    }

    protected abstract void ChildAwake();

    public abstract void Enter();
    public abstract void Exit();
}
