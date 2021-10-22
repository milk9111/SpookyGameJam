using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitSoundPlayer))]
public class Music : MonoBehaviour
{
    public AudioClip song;
    public float volume;

    private UnitSoundPlayer _soundPlayer;
    private bool _isPlaying;

    void Awake()
    {
        _soundPlayer = GetComponent<UnitSoundPlayer>();
    }

    void Update()
    {
        if (!_soundPlayer.IsPlaying())
        {
            _soundPlayer.PlayOneShot(song, volume);
        }
    }
}
