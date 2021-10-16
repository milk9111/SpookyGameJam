using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool mute;

    private float _startingVolume;

    private void Awake()
    {
        _startingVolume = AudioListener.volume;
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
