using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitSoundPlayer))]
public class MonsterTimer : MonoBehaviour
{
    public float monsterTimerSeconds;
    public float endTime { get; private set; }

    private UnitSoundPlayer _soundPlayer;
    private bool _isDone;

    public Action onComplete;
    
    private void Awake()
    {
        _soundPlayer = GetComponent<UnitSoundPlayer>();
        endTime = Time.time + monsterTimerSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= endTime && !_isDone)
        {
            _isDone = true;
            onComplete();
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), $"{(Time.time < endTime ? endTime - Time.time : 0f)}");
    }
}
