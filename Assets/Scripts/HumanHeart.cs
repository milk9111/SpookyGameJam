using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitSoundPlayer))]
public class HumanHeart : MonoBehaviour
{
    public AudioClip heartBeatClip;
    public float volume;

    private UnitSoundPlayer _soundPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        _soundPlayer = GetComponent<UnitSoundPlayer>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (!_soundPlayer.IsPlaying())
        {
            _soundPlayer.PlayOneShot(heartBeatClip, volume);
        }
    }
}
