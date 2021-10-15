using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public AudioClip bloodSplatterClip;
    public AudioClip screamClip;

    private Animator _animator;
    private UnitSoundPlayer _soundPlayer;
    private MonsterTimer _monsterTimer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _soundPlayer = GetComponent<UnitSoundPlayer>();
    }

    private void Start()
    {
        _monsterTimer = FindObjectOfType<MonsterTimer>();
        _monsterTimer.onComplete += Execute;
    }

    public void Execute()
    {
        _animator.SetTrigger("GameOver");
    }

    public void BloodSplatter()
    {
        _soundPlayer.PlayOneShot(bloodSplatterClip);
    }

    public void Scream()
    {
        _soundPlayer.PlayOneShot(screamClip);
    }
}
