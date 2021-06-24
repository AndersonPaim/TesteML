using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        Initialize();
        SetupDelegates();
    }

    private void OnDestroy()
    {
        RemoveDelegates();
    }

    private void SetupDelegates()
    {
        GameManager.sInstance.BowController.OnPlayerDataUpdate += ReceiveBowData;
        GameManager.sInstance.PlayerController.OnPlayerDataUpdate += ReceivePlayerData;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.BowController.OnPlayerDataUpdate -= ReceiveBowData;
        GameManager.sInstance.PlayerController.OnPlayerDataUpdate -= ReceivePlayerData;
    }

    private void Initialize()
    {
        _animator = GetComponent<Animator>();
    }

    private void ReceiveBowData(PlayerData playerData) //receive bow data from player struct
    {
        Aim(playerData.Aim);
        Shoot(playerData.Shoot);
        ChangeArrow(playerData.ChangeArrow);
        TakeDamage(playerData.TakeDamage);
    }

    private void ReceivePlayerData(PlayerData playerData) //receive player data from player struct
    {
        TakeDamage(playerData.TakeDamage);
    }

    private void Aim(bool isAiming)
    {
        _animator.SetBool(PlayerAnimationParameters.isAiming, isAiming);
    }

    private void Shoot(bool isShooting)
    {
        _animator.SetBool(PlayerAnimationParameters.isShooting, isShooting);
    }

    private void ChangeArrow(bool isChangingArrow)
    {
        if(isChangingArrow)
        {
            _animator.SetTrigger(PlayerAnimationParameters.changeArrow);
        }
    }

    private void TakeDamage(bool isTakingDamage)
    {
        if(isTakingDamage)
        {
            _animator.SetTrigger(PlayerAnimationParameters.takeDamage);
        }
    }
}
