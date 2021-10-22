using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public AudioClip laughterClip;
    public AudioClip lungeClip;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private UnitSoundPlayer _soundPlayer;
    private GameOver _gameOver;
    private MonsterTimer _monsterTimer;
    private Book _book;

    public bool end { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _soundPlayer = GetComponent<UnitSoundPlayer>();
    }

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _gameOver = FindObjectOfType<GameOver>();
        _monsterTimer = FindObjectOfType<MonsterTimer>();
        _monsterTimer.onComplete += Execute;

        _book = FindObjectOfType<Book>();
    }

    public void Execute()
    {
        end = true;
        _book.LowerBookAnim(ExecuteNextStep);
    }

    public void ExecuteNextStep()
    {
        _animator.SetTrigger("End");
    }

    public void GameOver()
    {
        _gameOver.Execute();
    }

    public void LaughterSound()
    {
        _soundPlayer.PlayOneShot(laughterClip);
    }

    public void LungeSound()
    {
        _soundPlayer.PlayOneShot(lungeClip);
    }

    public void IncreaseSorting()
    {
        _spriteRenderer.sortingLayerName = "Bed";
    }
}
