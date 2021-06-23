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
    [SerializeField] private AudioClip _hitAudio;
    [SerializeField] private AudioClip _damageAudio;

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
        if(enemyData.hasHittedPlayer)
        {
            HitSound();
        }
        if (enemyData.isAttacking && !_isAttacking)
        {
            AttackSound();
        }
        if (enemyData.isTakingDamage && !_isTakingDamage)
        {
            TakeDamageSound();
        }
        if(enemyData.isWalking)
        {
            WalkSound();
        }

        _isAttacking = enemyData.isAttacking;  //saving current data localy to avoid playing the same audio twice
        _isTakingDamage = enemyData.isTakingDamage;
    }

    private void WalkSound()
    {
        PlayFootstepsAudio(_footstepsAudio, _footstepsAudioSource, 1);
    }
    
    private void AttackSound()
    {
        PlayAudio(_attackAudio, _audioMixer, 0.7f, 1);
    }

    private void HitSound()
    {
        PlayAudio(_hitAudio, _audioMixer, 1, 1);
    }

    private void TakeDamageSound()
    {
        Debug.Log("SOFREU DANO");
    }

}
