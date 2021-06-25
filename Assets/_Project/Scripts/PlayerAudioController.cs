using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class PlayerAudioController : AudioController
{
    [SerializeField] protected AudioSource _footstepsAudioSource;

    [SerializeField] protected AudioMixerGroup _audioMixer;

    [SerializeField] private AudioClip _footstepsAudio;
    [SerializeField] private AudioClip _bowLoadingAudio;
    [SerializeField] private AudioClip _arrowEquipAudio;
    [SerializeField] private AudioClip _arrowShootAudio;
    [SerializeField] private AudioClip _healingAudio;
    [SerializeField] private AudioClip _arrowDamageAudio;
    [SerializeField] private AudioClip _collectArrowAudio;


    private bool _isAttacking;
    private bool _isAiming;

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
        GameManager.sInstance.PlayerController.OnPlayerDataUpdate += ReceivePlayerData;
        GameManager.sInstance.PlayerController.OnReceiveHealing += HealingAudio;
        EnemyArrow.OnArrowDamage += ArrowDamage;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.PlayerController.OnPlayerDataUpdate -= ReceivePlayerData;
        GameManager.sInstance.PlayerController.OnReceiveHealing -= HealingAudio;
        EnemyArrow.OnArrowDamage -= ArrowDamage;
    }

    private void ReceivePlayerData(PlayerData playerData)
    {
        MovementAudio(playerData.Movement, playerData.OnGround);
       /* if (enemyData.isAttacking && !_isAttacking)
        {
            AttackSound();
        }
  
        _isAttacking = enemyData.isAttacking;  //saving current data localy to avoid playing the same audio twice*/
    }

    private void ArrowDamage() //player receive damage from an arrow
    {
        PlayAudio(_arrowDamageAudio, _audioMixer, 1, 1);
    }

    private void HealingAudio()
    {
        PlayAudio(_healingAudio, _audioMixer, 1, 0);
    }

    private void ArrowEquipAudio() //play on player aim animation event
    {
        PlayAudio(_arrowEquipAudio, _audioMixer, 1, 0);
    }

    private void BowLoadingAudio() //play on player loading animation event
    {
        PlayAudio(_bowLoadingAudio, _audioMixer, 1, 0);
    }

    private void ArrowShootAudio() //play on player shoot animation event
    {
        PlayAudio(_arrowShootAudio, _audioMixer, 0.7f, 0);
    }

    private void CollectArrowAudio()
    {
        PlayAudio(_collectArrowAudio, _audioMixer, 1, 0);
    }

    private void MovementAudio(Vector2 movement, bool isGrounded) //footsteps audio //TODO SOM DE CORRIDA
    {
        if(movement.x != 0 && isGrounded || movement.y != 0 && isGrounded)
        {
            PlayFootstepsAudio(_footstepsAudio, _footstepsAudioSource, 1);
        }
        else
        {
            StopAudio(_footstepsAudioSource);
        }
    }
}
