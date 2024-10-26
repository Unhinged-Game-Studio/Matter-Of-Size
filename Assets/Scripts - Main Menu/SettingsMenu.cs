using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
using System;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI masterIField;
    [SerializeField] TextMeshProUGUI musicIField;
    [SerializeField] TextMeshProUGUI sfxIField;
    [SerializeField] TextMeshProUGUI mouseIField;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider mouseSlider;

    private float _mouseSensitivity;
    private string[] _options;
    private Action<float>[] _setters;
    private Action[] _loaders;

    private void Start()
    {
        _options = new string[] {"masterVolume", "musicVolume", "sfxVolume", "mouseSensitivity"};
        _setters = new Action<float>[]
        {
            MasterVolume,
            MusicVolume,
            SFXVolume,
            MouseSensitivity
        };
        _loaders = new Action[]
        {
            LoadMaster,
            LoadMusic,
            LoadSFX,
            LoadMouse
        };
        for (int i = 0; i < _options.Length; i++)
        {
            if (PlayerPrefs.HasKey(_options[i]))
                _loaders[i]();
            else
                _setters[i](0.5f);
        }
    }

    public void MasterVolume (float volume)
    {
        audioMixer.SetFloat("master", Mathf.Log10(volume) * 20);
        masterIField.text = ((int)(volume * 100)).ToString();
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    private void LoadMaster()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        MasterVolume(masterSlider.value);
    }
    
    public void MusicVolume (float volume)
    {
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        musicIField.text = ((int)(volume * 100)).ToString();
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    
    private void LoadMusic()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        MusicVolume(musicSlider.value);
    }
    public void SFXVolume (float volume)
    {
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        sfxIField.text = ((int)(volume * 100)).ToString();
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadSFX()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SFXVolume(SFXSlider.value);
    }

    public void MouseSensitivity (float amount)
    {
        _mouseSensitivity = Mathf.Log10(amount) * 20;
        mouseIField.text = ((int)(amount * 100)).ToString();
        PlayerPrefs.SetFloat("mouseSensitivity", amount);
    }

    private void LoadMouse()
    {
        mouseSlider.value = PlayerPrefs.GetFloat("mouseSensitivity");
        MouseSensitivity(mouseSlider.value);
    }
}
