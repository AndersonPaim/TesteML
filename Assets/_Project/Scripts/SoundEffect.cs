using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]

public class SoundEffect
{
    public AudioTags audioTag;

    public AudioClip audioClip;

    public AudioMixerGroup audioMixer;

    public float volume;

    public bool is3D;
}
