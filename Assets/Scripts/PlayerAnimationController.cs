using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;

    private bool _isAiming = false;

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
        GameManager.sInstance.BowController.OnPlayerDataUpdate += ReceiveInputs;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.BowController.OnPlayerDataUpdate -= ReceiveInputs;
    }


    private void Initialize()
    {
        _animator = GetComponent<Animator>();
    }

    private void ReceiveInputs(PlayerData playerData)
    {
        Aim(playerData.Aim);
        Shoot(playerData.Shoot);
        ChangeArrow(playerData.ChangeArrow);
    }

    private void Aim(bool isAiming)
    {
        _isAiming = isAiming;
        _animator.SetBool(PlayerAnimationParameters.ISAIMING, isAiming);
    }

    private void Shoot(bool isShooting)
    {
        if(isShooting && _isAiming)
        {
            _animator.SetTrigger(PlayerAnimationParameters.SHOOT);
        }
    }

    private void ChangeArrow(bool isChangingArrow)
    {
        if(isChangingArrow)
        {
            _animator.SetTrigger(PlayerAnimationParameters.CHANGEARROW);
        }
    }
}
