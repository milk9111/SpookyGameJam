using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public AudioClip bloodSplatterClip;
    public AudioClip screamClip;

    private Animator _animator;
    private UnitSoundPlayer _soundPlayer;
    private CompletionButtons _completionButtons;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _soundPlayer = GetComponent<UnitSoundPlayer>();
    }

    private void Start()
    {
        _completionButtons = FindObjectOfType<CompletionButtons>();
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
        _soundPlayer.PlayOneShot(screamClip, 0.5f);
    }

    public void ShowButtons()
    {
        _completionButtons.Show();
    }
}
