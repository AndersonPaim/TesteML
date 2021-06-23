using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameTimerText;
    [SerializeField] private TextMeshProUGUI _startTimerText;
    [SerializeField] private TextMeshProUGUI _piercingArrowText;
    [SerializeField] private TextMeshProUGUI _explosiveArrowText;


    [SerializeField] private Slider _healthBar;

    private Dictionary<ObjectsTag, TextMeshProUGUI> _arrowInventoryText;


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
        _arrowInventoryText = new Dictionary<ObjectsTag, TextMeshProUGUI>();
        _arrowInventoryText.Add(ObjectsTag.PiercingArrow, _piercingArrowText);
        _arrowInventoryText.Add(ObjectsTag.ExplosiveArrow, _explosiveArrowText);
    }

    private void SetupDelegates()
    {
        GameManager.sInstance.Timer.OnUpdateGameTimer += GameTimer;
        GameManager.sInstance.Timer.OnUpdateStartTimer += StartTimer;
        GameManager.sInstance.PlayerController.OnUpdateHealth += HealthBar;
        GameManager.sInstance.BowController.OnUpdateInventory += UpdateArrowInventory;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.Timer.OnUpdateGameTimer -= GameTimer;
        GameManager.sInstance.Timer.OnUpdateStartTimer -= StartTimer;
        GameManager.sInstance.PlayerController.OnUpdateHealth -= HealthBar;
        GameManager.sInstance.BowController.OnUpdateInventory -= UpdateArrowInventory;
    }

    private void UpdateArrowInventory(Dictionary<ObjectsTag, float> arrowInventory)
    {
        foreach(System.Collections.Generic.KeyValuePair<ObjectsTag, float> arrow in arrowInventory)
        {
            _arrowInventoryText[arrow.Key].text = arrow.Value.ToString();
        }
    }

    private void HealthBar(float health, float maxHealth)
    {
        _healthBar.maxValue = maxHealth;
        _healthBar.value = health;
    }

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