using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


public class EnemyAudioController : MonoBehaviour 
{
    [SerializeField] private Enemy _enemy;

    [SerializeField] private AudioSource _footstepsAudioSource;

    [SerializeField] private List<SoundEffect> _soundEffectList;

    private Dictionary<AudioTags, SoundEffect> _soundEffects; 

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
        //initialize sound effects dictionary
        _soundEffects = new Dictionary<AudioTags, SoundEffect>();

        foreach(SoundEffect soundEffect in _soundEffectList)
        {
            _soundEffects.Add(soundEffect.audioTag, soundEffect);
        }
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
        AudioController.sInstance.PlayFootstepsAudio(_soundEffects[AudioTags.Walk], transform.position, _footstepsAudioSource); 
    }
    
    private void AttackSound() //Play on enemies attack animation event
    {
        AudioController.sInstance.PlayAudio(_soundEffects[AudioTags.EnemyAttack], transform.position); 
    }

    private void HitSound()
    {
        AudioController.sInstance.PlayAudio(_soundEffects[AudioTags.EnemyHit], transform.position); 
    }
}