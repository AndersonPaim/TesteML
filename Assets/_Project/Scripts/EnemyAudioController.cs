using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class EnemyAudioController : AudioController
{
    [SerializeField] private Enemy _enemy;

    [SerializeField] private AudioSource _footstepsAudioSource;

    [SerializeField] private AudioClip _footstepsAudio;
    [SerializeField] private AudioClip _attackAudio;
    [SerializeField] private AudioClip _damageAudio;
    [SerializeField] private AudioClip _hitAudio;

    [SerializeField] private AudioMixerGroup _audioMixer;

    private bool _isAttacking;
    private bool _isTakingDamage;

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
        _enemy.OnUpdateEnemyData += ReceiveEnemyData;
    }

    private void RemoveDelegates()
    {
        _enemy.OnUpdateEnemyData -= ReceiveEnemyData;
    }

    private void ReceiveEnemyData(EnemyData enemyData)
    {
        if(enemyData.HittedPlayer)
        {
            HitSound();
        }
        if(enemyData.Walking)
        {
            WalkSound();
        }

        _isAttacking = enemyData.Attacking;  //saving current data localy to avoid playing the same audio twice
    }

    private void WalkSound()
    {
        PlayFootstepsAudio(_footstepsAudio, _footstepsAudioSource, 1);
    }
    
    private void AttackSound() //Play on enemies attack animation event
    {
        Debug.Log("CHEGOU");
        PlayAudio(_attackAudio, _audioMixer, 0.7f, 1);
    }

    private void HitSound()
    {
        PlayAudio(_hitAudio, _audioMixer, 1, 1);
    }
}
