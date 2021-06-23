using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameMenus : MonoBehaviour
{
    public delegate void PauseHandler(bool isPaused);
    public PauseHandler OnPause;

    public delegate void SceneHandler(string scene);
    public static SceneHandler OnSetScene;
    public static SceneHandler OnRestartScene;

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _finishMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _loadingScreen;

    [SerializeField] private Slider _loadingBar;

    [SerializeField] private TextMeshProUGUI _loadingProgressText;

    private bool _isPaused = false;
    private bool _canResume = true;

    private void Start()
    {
        SetupDelegates();
        Resume();
    }

    private void OnDestroy()
    {
        RemoveDelegates();
    }

    private void SetupDelegates()
    {
        GameManager.sInstance.InputListener.OnPause += PauseInput;
        GameManager.sInstance.OnFinish += Finish;
        GameManager.sInstance.OnGameOver += GameOver;
        GameManager.sInstance.SceneController.OnUpdateProgress += LoadingScreen;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.InputListener.OnPause -= PauseInput;
        GameManager.sInstance.OnFinish -= Finish;
        GameManager.sInstance.OnGameOver -= GameOver;
        GameManager.sInstance.SceneController.OnUpdateProgress -= LoadingScreen;
    }

    private void PauseInput()
    {
        if (_isPaused == false && _canResume == true)
        {
            Pause();
        }
        else if (_isPaused == true && _canResume == true)
        {
            Resume();
        }
    }

    private void LoadingScreen(float progress)
    {
        _loadingBar.value = progress;
        _loadingProgressText.text = (progress * 100).ToString() + "%" ;
    }


    public void Pause()
    {
        OnPause?.Invoke(true);
        _isPaused = true;
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameOver()
    {
        OnPause?.Invoke(true);
        _canResume = false;
        Time.timeScale = 0;
        _gameOverMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Finish()
    {
        OnPause?.Invoke(true);
        _canResume = false;
        Time.timeScale = 0;
        _finishMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        OnPause?.Invoke(false);
        _isPaused = false;
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Settings()
    {
        _pauseMenu.SetActive(false);
        _settingsMenu.SetActive(true);
        _canResume = false;
    }

    public void BackButton()
    {
        _pauseMenu.SetActive(true);
        _settingsMenu.SetActive(false);
        _canResume = true;
    }

    public void Restart()
    {
        OnRestartScene?.Invoke("Game"); 
    }

    public void Quit()
    {
        _loadingScreen.SetActive(true);
        OnSetScene?.Invoke("Menu");
    }
}