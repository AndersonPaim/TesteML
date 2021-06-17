using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioController : MonoBehaviour
{
    public static AudioController sInstance;

   /* [SerializeField] private AudioClip _walkAudio;
    [SerializeField] private AudioClip _runAudio;
    [SerializeField] private AudioClip _jumpAudio;
    [SerializeField] private AudioClip teste;*/

    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private AudioSource _footstepsAudioSource;

   // [SerializeField] private AudioMixerGroup _playerMixer;


    private ObjectPooler _objectPooler;

    private AudioSource _audioSource;

    private float _volume;

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
        SetupDelegates();
    }

    private void Initialize()
    {
        SaveSystem.Load(); //TODO TEMPORARIO, COLOCAR NO MENU
        SaveData data = SaveSystem.localData;

        _audioMixer.SetFloat("effectsVolume", Mathf.Log10(data.soundfxVolume) * 20);

        _objectPooler = GameManager.sInstance.ObjectPooler;
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
        _audioMixer.SetFloat("effectsVolume", Mathf.Log10(volume) * 20);
        _volume = volume;
    }

    private void PauseAudio(bool isPaused)
    {
        if (isPaused)
        {
            _audioMixer.SetFloat("masterVolume", -80);
        }
        else
        {
            _audioMixer.SetFloat("masterVolume", 0);
        }
    }

    public void PlayAudio(AudioClip audio, float volume, AudioMixerGroup audioMixer)
    {
       /* GameObject obj = _objectPooler.SpawnFromPool(10);
        _audioSource = obj.GetComponent<AudioSource>();
        obj.transform.position = transform.position;

        _audioSource.outputAudioMixerGroup = audioMixer;

        _audioSource.clip = audio;
        _audioSource.volume = volume;*/
      
        //obj.GetComponent<AudioSourceObject>().Disable();
        //TODO DISABILITAR O OBJETO INSTANCIADO COM O AUDIO SOURCE

        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }
    private void PlayFootstepsAudio(AudioClip audio, float volume)
    {
        _footstepsAudioSource.clip = audio;
        _footstepsAudioSource.volume = volume;

        if (!_footstepsAudioSource.isPlaying)
        {
            _footstepsAudioSource.Play();
        }
    }

    
}
