using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioController : MonoBehaviour 
{
    [SerializeField] private AudioMixer _gameAudioMixer;

    public static AudioController sInstance;

    private ObjectPooler _objectPooler;
    private void Awake()
    {
        if (sInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            sInstance = this;
        }
    }

    private void Start() 
    {
        Initialize();
    }

    private void Initialize()
    {
        _objectPooler = GameManager.sInstance.ObjectPooler;

        SaveData data = SaveSystem.localData;

        _gameAudioMixer.SetFloat(AudioMixerParameters.finalVolume, Mathf.Log10(data.soundfxVolume) * 20); 
        _gameAudioMixer.SetFloat(AudioMixerParameters.effectsVolume, Mathf.Log10(data.soundfxVolume) * 20);
        _gameAudioMixer.SetFloat(AudioMixerParameters.musicVolume, Mathf.Log10(data.musicVolume) * 20);
    }

    private void EffectsVolume(float volume)
    {
        _gameAudioMixer.SetFloat(AudioMixerParameters.effectsVolume, Mathf.Log10(volume) * 20);
        _gameAudioMixer.SetFloat(AudioMixerParameters.finalVolume,  Mathf.Log10(volume) * 20);
    }

    private void PauseAudio(bool isPaused)
    {
        if (isPaused) //mute game on pause
        {
            _gameAudioMixer.SetFloat(AudioMixerParameters.effectsVolume, -80);
            _gameAudioMixer.SetFloat(AudioMixerParameters.musicVolume, -80);
        }
        else //return volume to normal when unpause
        {
            SaveData data = SaveSystem.localData;
            _gameAudioMixer.SetFloat(AudioMixerParameters.effectsVolume, data.soundfxVolume);
            _gameAudioMixer.SetFloat(AudioMixerParameters.musicVolume, data.musicVolume);
        }
    }

    private void SetupDelegates()
    {
        GameManager.sInstance.InGameMenus.OnPause += PauseAudio;
        GameManager.sInstance.InGameSettings.OnSetEffectsVolume += EffectsVolume;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.InGameMenus.OnPause -= PauseAudio;
        GameManager.sInstance.InGameSettings.OnSetEffectsVolume -= EffectsVolume;
    }

    public void PlayAudio(SoundEffect soundEffect, Vector3 position)  //create a object with the audiosource to play multiples audios at the same time
    {
      
        AudioSource audioSource;

        GameObject obj = _objectPooler.SpawnFromPool(ObjectsTag.AudioSource);
        audioSource = obj.GetComponent<AudioSource>();
        obj.transform.position = position;

        audioSource.outputAudioMixerGroup = soundEffect.audioMixer;

        audioSource.clip = soundEffect.audioClip;
        obj.SetActive(false); //restart object from object pooler to only play after receive audioclip
        obj.SetActive(true); 
        audioSource.volume = soundEffect.volume;

        if(soundEffect.is3D)
        {
            audioSource.spatialBlend = 1;
        }
        else
        {
            audioSource.spatialBlend = 0;
        }
      
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }


    public void PlayFootstepsAudio(SoundEffect soundEffect, Vector3 position, AudioSource audioSource)
    {
        audioSource.clip = soundEffect.audioClip;
        audioSource.volume = soundEffect.volume;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopAudio(AudioSource audioSource)
    {
        audioSource.Stop();
    }
}