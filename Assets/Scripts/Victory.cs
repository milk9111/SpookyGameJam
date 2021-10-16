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

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _soundPlayer = GetComponent<UnitSoundPlayer>();
    }

    private void Start()
    {
        _occultSymbolController = FindObjectOfType<OccultSymbolController>();
        _occultSymbolController.onVictory += Execute;
    }

    public void Execute()
    {
        _animator.SetTrigger("Victory");
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
