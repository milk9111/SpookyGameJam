using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool mute;
    public DifficultySO difficulty;

    private OccultSymbolController _occultSymbolController;
    private MonsterTimer _monsterTimer;
    private float _startingVolume;

    private void Awake()
    {
        _startingVolume = AudioListener.volume;
    }

    private void Start()
    {
        _occultSymbolController = FindObjectOfType<OccultSymbolController>();
        _monsterTimer = FindObjectOfType<MonsterTimer>();

        _occultSymbolController.totalSymbols = difficulty.symbolCount;
        _monsterTimer.monsterTimerSeconds = difficulty.monsterTimerSeconds;
        _monsterTimer.ResetEndTime();
    }

    private void Update()
    {
        if (mute)
        {
            Mute();
        } else
        {
            Unmute();
        }
    }

    public void Mute()
    {
        AudioListener.volume = 0;
    }

    public void Unmute()
    {
        AudioListener.volume = _startingVolume;
    }
}
