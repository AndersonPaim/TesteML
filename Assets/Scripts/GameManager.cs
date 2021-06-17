using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager sInstance;

    public delegate void GameOverHandler();
    public GameOverHandler OnGameOver;

    public delegate void FinishHandler();
    public FinishHandler OnFinish;

    [SerializeField] private PlayerController _playerController;

    [SerializeField] private InputListener _inputListener;

    [SerializeField] private InGameMenus _inGameMenus;

    [SerializeField] private CameraController _cameraController;

    [SerializeField] private Settings _settings;

    [SerializeField] private ObjectPooler _objectPooler;

    [SerializeField] private Timer _timer;

    [SerializeField] private BowController _bowController;

    [SerializeField] private GameObject _player;

    [SerializeField] private float _gameTimer; // game timer in seconds
    private void Awake()
    {
        if (sInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            sInstance = this;
        }
        SetupDelegates();
    }

    private void OnDestroy()
    {
        RemoveDelegates();
    }

    private void SetupDelegates()
    {
        _timer.OnFinishTimer += Finish;
        _playerController.OnPlayerDeath += GameOver;
    }

    private void RemoveDelegates()
    {
        _timer.OnFinishTimer -= Finish;
        _playerController.OnPlayerDeath -= GameOver;
    }

    public PlayerController PlayerController
    {
        get { return _playerController; }
    }

    public InputListener InputListener
    {
        get { return _inputListener; }
    }

    public InGameMenus InGameMenus
    {
        get { return _inGameMenus; }
    }

    public CameraController CameraController
    {
        get { return _cameraController; }
    }

    public Settings Settings
    {
        get { return _settings; }
    }

    public ObjectPooler ObjectPooler
    {
        get { return _objectPooler; }
    }

  /*  public Timer GetTimer()
    {
        return _timer;
    }*/

    public Timer Timer
    {
        get { return _timer; }
    }

    public BowController BowController
    {
        get { return _bowController; }
    }

    public GameObject Player
    {
        get { return _player; }
    }
    
    public float GameTimer
    {
        get { return _gameTimer; }
    }

    private void Finish()
    {
        OnFinish?.Invoke();
    }

    private void GameOver()
    {
        OnGameOver?.Invoke();
    }
}