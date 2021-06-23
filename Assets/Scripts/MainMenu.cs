using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public delegate void SceneHandler(string scene);
    public static SceneHandler OnSetScene;
    public static SceneHandler OnRestartScene;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _loadingScreen;

    [SerializeField] private Slider _loadingBar;

    [SerializeField] private TextMeshProUGUI _loadingProgressText;

    [SerializeField] private SceneController _sceneController; //TODO VER SE PRECISA COLOCAR ELE AQUI

    private void Awake()
    {
        Initialize();
        SetupDelegates();
    }

    private void OnDestroy() 
    {
        RemoveDelegates();
    }

    private void Initialize()
    {
        Cursor.lockState = CursorLockMode.None;
        SaveSystem.Load();
    }

    private void SetupDelegates()
    {
        _sceneController.OnUpdateProgress += LoadingScreen;
    }

    private void RemoveDelegates()
    {
        _sceneController.OnUpdateProgress -= LoadingScreen;
    }

    public void PlayButton()
    {
        //TODO AJEITAR A CENA DO LOADING
        _mainMenu.SetActive(false); //TODO COLOCAR ISSO NA CLASSE DO SCENECONTROLLER
        _loadingScreen.SetActive(true);
        OnSetScene?.Invoke("Game");
    }

    private void LoadingScreen(float progress)
    {
        _loadingBar.value = progress;
        _loadingProgressText.text = (progress * 100).ToString() + "%" ;
    }

    public void SettingsButton()
    {
        _settingsMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void BackButton()
    {
        _settingsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}