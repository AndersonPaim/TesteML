using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{   
    public delegate void FinishTimerHandler();
    public FinishTimerHandler OnFinishGameTimer;
    public FinishTimerHandler OnFinishCountdownTimer;

    public delegate void GameTimerHandler(float minutes, float seconds);
    public GameTimerHandler OnUpdateGameTimer;
    public GameTimerHandler OnUpdateStartTimer;

    [SerializeField] private GameObject _startTimerObject;
    [SerializeField] private GameObject _gameTimerObject;

    private bool _isPaused;
    private bool _gameStarted;

    private float _startTime;
    private float _gameTime;
    private float _minutes;
    private float _seconds;

    private void Start() 
    {
        Initialize();
    }

    private void Update()
    {
        if(!_isPaused && _gameStarted)
        {
            GameCountdown();
        }
        else if(!_isPaused)
        {
            StartCountdown();
        }
    }

    private void OnDestroy() 
    {
        RemoveDelegates();   
    }

    private void Initialize()
    {
        SaveData data = SaveSystem.localData;
        _gameTime = data.gameTime * 60;
        _startTime = data.startCountdown;
        
        StartCountdown();
    }

    private void SetupDelegates()
    {
        GameManager.sInstance.InputListener.OnPause += Pause;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.InputListener.OnPause -= Pause;
    }

    private void Pause()
    {
        if(_isPaused)
        {
            _isPaused = false;
        }
        else
        {
            _isPaused = true;
        }
    }

    private void StartCountdown()
    {   
        
        _startTime -= Time.deltaTime;
        _seconds = Mathf.FloorToInt(_startTime % 60);

        OnUpdateStartTimer?.Invoke(0, _seconds);

        if(_startTime <= 0)
        {
            _startTimerObject.SetActive(false);
            _gameTimerObject.SetActive(true);
            _gameStarted = true;
            OnFinishCountdownTimer?.Invoke();
        }
    }

    private void GameCountdown()
    {
        _gameTime -= Time.deltaTime;
        
        _minutes = Mathf.FloorToInt(_gameTime / 60);
        _seconds = Mathf.FloorToInt(_gameTime % 60);
        
        OnUpdateGameTimer?.Invoke(_minutes, _seconds);

        if(_gameTime <= 0)
        {
            OnFinishGameTimer?.Invoke();
        }

    }
}
