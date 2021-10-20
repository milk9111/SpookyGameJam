using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public AudioClip victoryClip;
    public AudioClip pencilClip;
    
    private Animator _animator;
    private UnitSoundPlayer _soundPlayer;

    private OccultSymbolController _occultSymbolController;
    private CompletionButtons _completionButtons;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _soundPlayer = GetComponent<UnitSoundPlayer>();
    }

    private void Start()
    {
        _occultSymbolController = FindObjectOfType<OccultSymbolController>();
        _occultSymbolController.onVictory += Execute;

        _completionButtons = FindObjectOfType<CompletionButtons>();
    }

    public void Execute()
    {
        Debug.Log("Executing Victory");
        _animator.SetTrigger("Victory");
    }

    public void ShowButtons()
    {
        _completionButtons.Show();
    }

    public void VictorySound()
    {
        _soundPlayer.PlayOneShot(victoryClip);
    }

    public void PencilSound()
    {
        _soundPlayer.PlayOneShot(pencilClip);
    }
}
