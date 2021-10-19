using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(UnitSoundPlayer))]
public class MonsterTimer : MonoBehaviour
{
    public float monsterTimerSeconds;
    public float endTime { get; private set; }

    public bool disabled;

    public float eventIntervalSeconds;

    public List<AbstractEvent> events;

    private UnitSoundPlayer _soundPlayer;
    private bool _isDone;
    private bool _activeEvent;

    private List<AbstractEvent> _usedEvents;

    private float _currentTime;

    public Action onComplete;
    
    private void Awake()
    {
        _soundPlayer = GetComponent<UnitSoundPlayer>();
        ResetEndTime();
        _usedEvents = new List<AbstractEvent>();
    }

    private void Start()
    {
        foreach (var ev in events)
        {
            ev.onExit += ExitCallback;
        }
    }

    public void ResetEndTime()
    {
        endTime = Time.time + monsterTimerSeconds;
        _currentTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (disabled)
        {
            return;
        }

        _currentTime += Time.deltaTime;

        if (_currentTime >= endTime && !_isDone)
        {
            _isDone = true;
            onComplete();
        }

        if (Time.unscaledTime % eventIntervalSeconds <= 0.01f && !_activeEvent)
        {
            var index = Random.Range(0, events.Count);

            _activeEvent = true;
            events[index].Enter();

            _usedEvents.Add(events[index]);
            events.RemoveAt(index);

            if (!events.Any())
            {
                events = _usedEvents;
                _usedEvents = new List<AbstractEvent>();
            }
        }
    }

    public void ExitCallback()
    {
        _activeEvent = false;
    }
}
