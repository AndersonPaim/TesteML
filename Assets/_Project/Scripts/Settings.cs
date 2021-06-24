using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public delegate void SetEffectsVolumeHandler(float volume);
    public SetEffectsVolumeHandler OnSetEffectsVolume;

    public delegate void SetMusicVolumeHandler(float volume);
    public SetMusicVolumeHandler OnSetMusicVolume;

    [SerializeField] private Slider _soundfxSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _gameTimeSlider;
    [SerializeField] private Slider _startCountdownSlider;
    
    [SerializeField] private TextMeshProUGUI _gameTimeText;
    [SerializeField] private TextMeshProUGUI _startCountdownText;


    [SerializeField] private float _maxGameTime;  //game time in minutes
    [SerializeField] private float _minGameTime;
    [SerializeField] private float _maxStartCountdown; //start countdown in seconds
    [SerializeField] private float _minStartCountdown;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        //load last settings from local save data
        SaveData data = SaveSystem.localData;
        _soundfxSlider.value = data.soundfxVolume;
        _musicSlider.value = data.musicVolume;
        _gameTimeSlider.value = data.gameTime;
        _startCountdownSlider.value = data.startCountdown;

        _gameTimeSlider.minValue = _minGameTime;
        _gameTimeSlider.maxValue = _maxGameTime;
        _startCountdownSlider.minValue = _minStartCountdown;
        _startCountdownSlider.maxValue = _maxStartCountdown;
    }

    public void SoundfxVolume(float volume) //on change slider value set sound effects volume value
    {
        SaveSystem.localData.soundfxVolume = volume;
        OnSetEffectsVolume?.Invoke(volume);
    }

    public void MusicVolume(float volume) //on change slider value set music volume value
    {
        SaveSystem.localData.musicVolume = volume;
        OnSetMusicVolume?.Invoke(volume);
    }

    public void StartCountdown(float time) //on change slider value set start countdown time value
    {
        _startCountdownText.text = _startCountdownSlider.value.ToString() + "sec";
        SaveSystem.localData.startCountdown = time;
    }

    public void GameTime(float time) //on change slider value set game time value
    {
        _gameTimeText.text = _gameTimeSlider.value.ToString() + "min";
        SaveSystem.localData.gameTime = time;
    }

    public void Save() //save changes on exit button 
    {
        SaveSystem.Save();
    }
}