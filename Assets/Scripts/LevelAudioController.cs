using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LevelAudioController : AudioController
{
    [SerializeField] private AudioClip _finishAudio;
    [SerializeField] private AudioClip _gameOverAudio;

    [SerializeField] private AudioMixerGroup _audioMixer;

    private void Start()
    {
        SetupDelegates();
        Initialize();
    }

    private void OnDestroy() 
    {
        RemoveDelegates();
    }

    private void Initialize()
    {
        _objectPooler = GameManager.sInstance.ObjectPooler;
    }

    private void SetupDelegates()
    {
        GameManager.sInstance.OnFinish += FinishAudio;
        GameManager.sInstance.OnGameOver += GameOverAudio;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.OnFinish -= FinishAudio;
        GameManager.sInstance.OnGameOver -= GameOverAudio;
    }

    private void FinishAudio()
    {
        PlayAudio(_finishAudio, _audioMixer, 0.3f, 0);
    }

    private void GameOverAudio()
    {
        PlayAudio(_gameOverAudio, _audioMixer, 0.3f, 0);
    }
}
