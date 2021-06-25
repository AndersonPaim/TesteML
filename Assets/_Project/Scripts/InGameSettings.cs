using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameSettings : MonoBehaviour
{
    public delegate void SetEffectsVolumeHandler(float volume);
    public SetEffectsVolumeHandler OnSetEffectsVolume;

    public delegate void SetMusicVolumeHandler(float volume);
    public static SetMusicVolumeHandler OnSetMusicVolume;

    [SerializeField] private Slider _soundfxSlider;
    [SerializeField] private Slider _musicSlider;

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

    public void Save() //save changes on exit button 
    {
        SaveSystem.Save();
    }
}