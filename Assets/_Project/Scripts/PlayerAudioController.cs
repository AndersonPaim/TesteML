using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


public class PlayerAudioController : MonoBehaviour 
{
    [SerializeField] private AudioSource _footstepsAudioSource;

    [SerializeField] private List<SoundEffect> _soundEffectList;

    private Dictionary<AudioTags, SoundEffect> _soundEffects; 

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
        //initialize sound effects dictionary
        _soundEffects = new Dictionary<AudioTags, SoundEffect>();

        foreach(SoundEffect soundEffect in _soundEffectList)
        {
            _soundEffects.Add(soundEffect.audioTag, soundEffect);
        }
    }

    private void SetupDelegates()
    {
        GameManager.sInstance.PlayerController.OnPlayerDataUpdate += ReceivePlayerData;
        GameManager.sInstance.PlayerController.OnReceiveHealing += HealingAudio;
        GameManager.sInstance.PlayerController.OnPlayerDeath += DeathAudio;
        GameManager.sInstance.OnFinish += WinAudio;
        EnemyArrow.OnArrowDamage += ArrowDamage;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.PlayerController.OnPlayerDataUpdate -= ReceivePlayerData;
        GameManager.sInstance.PlayerController.OnReceiveHealing -= HealingAudio;
        GameManager.sInstance.PlayerController.OnPlayerDeath -= DeathAudio;
        GameManager.sInstance.OnFinish -= WinAudio;
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
        AudioController.sInstance.PlayAudio(_soundEffects[AudioTags.ArrowDamage], transform.position); 
    }

    private void HealingAudio()
    {
        AudioController.sInstance.PlayAudio(_soundEffects[AudioTags.Healing], transform.position); 
    }

    private void ArrowEquipAudio() //play on player aim animation event
    {
        AudioController.sInstance.PlayAudio(_soundEffects[AudioTags.ArrowEquip], transform.position); 
    }

    private void BowLoadingAudio() //play on player loading animation event
    {
        AudioController.sInstance.PlayAudio(_soundEffects[AudioTags.ArrowLoading], transform.position); 
    }

    private void ArrowShootAudio() //play on player shoot animation event
    {
        AudioController.sInstance.PlayAudio(_soundEffects[AudioTags.ArrowShoot], transform.position); 
    }

    private void CollectArrowAudio()
    {
        //TODO
    }

    private void DeathAudio()
    {
        AudioController.sInstance.PlayAudio(_soundEffects[AudioTags.Death], transform.position); 
    }

    private void WinAudio()
    {
        AudioController.sInstance.PlayAudio(_soundEffects[AudioTags.Win], transform.position); 
    }

    private void MovementAudio(Vector2 movement, bool isGrounded) //footsteps audio
    {
        if(movement.x != 0 && isGrounded || movement.y != 0 && isGrounded)
        {
            AudioController.sInstance.PlayFootstepsAudio(_soundEffects[AudioTags.Walk], transform.position, _footstepsAudioSource); 
        }
        else
        {
            AudioController.sInstance.StopAudio(_footstepsAudioSource);
        }
    }
}