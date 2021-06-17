using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settingsMenu;

    public void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        Cursor.lockState = CursorLockMode.None;
        SaveSystem.Load();
    }

    public void PlayButton()
    {
        SceneController.SetScene("Game");
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