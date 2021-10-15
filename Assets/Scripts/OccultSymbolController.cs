using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitSoundPlayer))]
public class OccultSymbolController : MonoBehaviour
{
    public OccultSymbol symbol;
    public AudioClip successClip;
    public AudioClip messedUpClip;

    private UnitSoundPlayer _soundPlayer;

    private MonsterTimer _monsterTimer;

    private void Awake()
    {
        _soundPlayer = GetComponent<UnitSoundPlayer>();
    }

    private void Start()
    {
        _monsterTimer = FindObjectOfType<MonsterTimer>();
        _monsterTimer.onComplete += OnCompleteCallback;
        symbol.Init(SuccessCallback, MessedUpCallback);   
    }

    public void OnCompleteCallback()
    {
        symbol.StopDrawing();
    }

    public void SuccessCallback()
    {
        _soundPlayer.PlayOneShot(successClip, 0.5f);
    }

    public void MessedUpCallback()
    {
        _soundPlayer.PlayOneShot(messedUpClip);
    }
}
