using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]

public class Music
{
    public AudioTags audioTag;

    public AudioClip audioClip;

    public AudioMixerGroup audioMixer;

    public float volume;
}
