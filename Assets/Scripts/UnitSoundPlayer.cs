using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UnitSoundPlayer : MonoBehaviour
{
    private AudioSource _source;

    // Start is called before the first frame update
    void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip, float volume = 1f)
    {
        _source.PlayOneShot(clip, volume);
    }

    public void StopOneShot()
    {
        _source.Stop();
    }

    public void PlayRepeating(AudioClip clip, float volume = 1f)
    {
        _source.loop = true;
        _source.volume = volume;
        _source.clip = clip;
        _source.Play();
    }

    public bool IsPlaying()
    {
        return _source.isPlaying;
    }

    public void StopRepeating()
    {
        _source.Stop();
        _source.loop = false;
        _source.volume = 1f;
        _source.clip = null;
    }

    public static void Mute()
    {
        AudioListener.volume = 0;
    }

    public static void Unmute()
    {
        AudioListener.volume = 1;
    }
}