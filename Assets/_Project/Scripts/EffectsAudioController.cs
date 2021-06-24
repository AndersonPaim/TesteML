using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EffectsAudioController : AudioController
{
    [SerializeField] private AudioClip _finishAudio;
    [SerializeField] private AudioClip _gameOverAudio;
    [SerializeField] private AudioClip _explosiveArrowAudio;

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
        ExplosiveArrow.OnArrowHit += ArrowExplosionAudio;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.OnFinish -= FinishAudio;
        GameManager.sInstance.OnGameOver -= GameOverAudio;
        ExplosiveArrow.OnArrowHit -= ArrowExplosionAudio;
    }

    private void FinishAudio() //Play when game ends
    {
        PlayAudio(_finishAudio, _audioMixer, 0.3f, 0);
    }

    private void GameOverAudio() //Play when player dies //TODO VER VOLUMES
    {
        PlayAudio(_gameOverAudio, _audioMixer, 0.3f, 0);
    }

    private void ArrowExplosionAudio(Vector3 pos) //TODO TA UM POUCO BAIXO
    {
        PlayAudioAtPosition(_explosiveArrowAudio, _audioMixer, 1, pos);
    }
}
