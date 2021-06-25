using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private Music _bgMusic;

    [SerializeField] private Settings _settings;

    private void Start()
    {
        Initialize();
        SetupDelegates();
    }

    private void OnDestroy()
    {
        RemoveDelegates();
    }
    public void PlayMusic(Music music)
    {
        _audioSource.clip = music.audioClip;
        _audioSource.volume = music.volume;
        _audioSource.Play();
    }

    private void SetupDelegates()
    {
        _settings.OnSetMusicVolume += SetMusicVolume;
    }

    private void RemoveDelegates()
    {
        _settings.OnSetMusicVolume -= SetMusicVolume;
    }

    private void Initialize()
    {
        _audioMixer.SetFloat(AudioMixerParameters.musicVolume, Mathf.Log10 (SaveSystem.localData.musicVolume) * 20);

        PlayMusic(_bgMusic);
    }

    private void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat(AudioMixerParameters.musicVolume, Mathf.Log10(volume) * 20);
    }
}