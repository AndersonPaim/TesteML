using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameTimerText;
    [SerializeField] private TextMeshProUGUI _startTimerText;
    //[SerializeField] private TextMeshProUGUI _ammoText;
    
    [SerializeField] private Slider _healthBar;

    private void Start()
    {
        SetupDelegates();
    }

    private void OnDestroy()
    {
        RemoveDelegates();
    }

    private void SetupDelegates()
    {
        GameManager.sInstance.Timer.OnUpdateGameTimer += GameTimer;
        GameManager.sInstance.Timer.OnUpdateStartTimer += StartTimer;
        GameManager.sInstance.PlayerController.OnUpdateHealth += HealthBar;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.Timer.OnUpdateGameTimer -= GameTimer;
        GameManager.sInstance.Timer.OnUpdateStartTimer -= StartTimer;
        GameManager.sInstance.PlayerController.OnUpdateHealth -= HealthBar;
    }

    private void HealthBar(float health, float maxHealth)
    {
        _healthBar.maxValue = maxHealth;
        _healthBar.value = health;
    }

    /*private void UpdateAmmo(float ammo)
    {
        _ammoText.text = ammo.ToString();
    }*/

    private void GameTimer(float minutes, float seconds)
    {
        if(seconds < 10)
        {
            _gameTimerText.text = minutes.ToString() + ":" + "0" + seconds.ToString();
        }
        else
        {
            _gameTimerText.text = minutes.ToString() + ":" + seconds.ToString();
        }
    }

    private void StartTimer(float minutes, float seconds)
    {
        if(seconds == 0)
        {
            _startTimerText.text = "GO!";
        }
        else
        {
            _startTimerText.text = seconds.ToString();
        }
    }

}