using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class PlayerAudioController : AudioController
{
    [SerializeField] protected AudioSource _footstepsAudioSource;

    [SerializeField] protected AudioMixerGroup _audioMixer;

    [SerializeField] private AudioClip _footstepsAudio;
    [SerializeField] private AudioClip _bowAimAudio;
    [SerializeField] private AudioClip _arrowShootAudio;
    [SerializeField] private AudioClip _healingAudio;

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
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.PlayerController.OnPlayerDataUpdate -= ReceivePlayerData;
        GameManager.sInstance.PlayerController.OnReceiveHealing -= HealingAudio;
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

    private void HealingAudio()
    {
        PlayAudio(_healingAudio, _audioMixer, 1, 0);
    }

    private void BowAimAudio() //play on player aim animation event
    {
        PlayAudio(_bowAimAudio, _audioMixer, 1, 0);
    }

    private void ArrowShootAudio()
    {
        PlayAudio(_arrowShootAudio, _audioMixer, 1, 0);
    }

    private void MovementAudio(Vector2 movement, bool isGrounded)
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