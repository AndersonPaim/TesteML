using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer _gameAudioMixer;

    protected ObjectPooler _objectPooler;
    
    private AudioSource _audioSource;

    private float _volume;


    private void Start() 
    {
        Initialize();
        SetupDelegates();
    }

    protected void PlayAudio(AudioClip audioClip, AudioMixerGroup audioMixer, float volume, float spatialBlend)  //create a object with the audiosource to play multiples audios at the same time
    {
      
        AudioSource audioSource;

        GameObject obj = _objectPooler.SpawnFromPool(ObjectsTag.AudioSource);
        audioSource = obj.GetComponent<AudioSource>();
        obj.transform.position = transform.position;

        audioSource.outputAudioMixerGroup = audioMixer;

        audioSource.clip = audioClip;
        obj.SetActive(false); //restart object from object pooler to only play after receive audioclip
        obj.SetActive(true); 
        audioSource.volume = volume;
        audioSource.spatialBlend = spatialBlend; //0 for 2d audio and 1 for 3d audio
      
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    protected void PlayFootstepsAudio(AudioClip audio, AudioSource audioSource, float volume)
    {
        audioSource.clip = audio;
        audioSource.volume = volume;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    protected void StopAudio(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    private void Initialize()
    {
        SaveData data = SaveSystem.localData;

        _gameAudioMixer.SetFloat("effectsVolume", Mathf.Log10(data.soundfxVolume) * 20);
    }

    private void SetupDelegates()
    {
        GameManager.sInstance.InGameMenus.OnPause += PauseAudio;
        GameManager.sInstance.Settings.OnSetEffectsVolume += EffectsVolume;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.InGameMenus.OnPause -= PauseAudio;
        GameManager.sInstance.Settings.OnSetEffectsVolume -= EffectsVolume;
    }

    private void EffectsVolume(float volume)
    {
        _gameAudioMixer.SetFloat(AudioMixerParameters.effectsVolume, Mathf.Log10(volume) * 20);
        _gameAudioMixer.SetFloat(AudioMixerParameters.finalSoundEffects,  Mathf.Log10(volume) * 20);
        _volume = volume;
    }

    private void PauseAudio(bool isPaused)
    {
        if (isPaused)
        {
           // _gameAudioMixer.SetFloat(AudioMixerParameters.masterVolume, -80);
            _gameAudioMixer.SetFloat(AudioMixerParameters.effectsVolume, -80);
            _gameAudioMixer.SetFloat(AudioMixerParameters.musicVolume, -80);
        }
        else
        {
            //_gameAudioMixer.SetFloat(AudioMixerParameters.masterVolume, 0);
            _gameAudioMixer.SetFloat(AudioMixerParameters.effectsVolume, 0);
            _gameAudioMixer.SetFloat(AudioMixerParameters.musicVolume, 0);
        }
    }
    
}
