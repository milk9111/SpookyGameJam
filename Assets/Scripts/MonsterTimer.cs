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

    public int symbolSuccessAmount;
    public int symbolMessUpAmount;

    public List<AbstractEvent> events;

    private UnitSoundPlayer _soundPlayer;
    private bool _isDone;
    private bool _activeEvent;

    private OccultSymbolController _occultSymbolController;

    private List<AbstractEvent> _usedEvents;

    public float currentTime;

    private float _startTime;

    private int _firstLoopCount;

    public Action onComplete;
    
    private void Awake()
    {
        _soundPlayer = GetComponent<UnitSoundPlayer>();
        ResetEndTime();
        _usedEvents = new List<AbstractEvent>();
        _firstLoopCount = 0;
    }

    private void Start()
    {
        _occultSymbolController = FindObjectOfType<OccultSymbolController>();
        _occultSymbolController.onSuccess = SuccessCallback;
        _occultSymbolController.onMessUp = MessUpCallback;
        _occultSymbolController.onCanDoNextSymbol = CanDoNextSymbolCallback;

        foreach (var ev in events)
        {
            ev.onExit += ExitCallback;
        }
    }

    public void ResetEndTime()
    {
        endTime = Time.time + monsterTimerSeconds;
        currentTime = Time.time + (monsterTimerSeconds / 2);
        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (disabled)
        {
            return;
        }

        currentTime += Time.deltaTime;

        if (currentTime >= endTime && !_isDone)
        {
            _isDone = true;
            onComplete();
        }

        if (_firstLoopCount > 10 && Time.unscaledTime % eventIntervalSeconds <= 0.01f && !_activeEvent && !_isDone)
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

        _firstLoopCount++;
    }

    public void SuccessCallback()
    {
        currentTime -= symbolSuccessAmount;
        if (currentTime <= _startTime)
        {
            _occultSymbolController.Victory();
        }
    }

    public void MessUpCallback()
    {
        currentTime += symbolMessUpAmount;
    }

    public bool CanDoNextSymbolCallback()
    {
        return currentTime > _startTime && currentTime < endTime;
    }

    public void ExitCallback()
    {
        _activeEvent = false;
    }
}
