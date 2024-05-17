using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsScript : MonoBehaviour
{
    public GameObject Settings;
    public  SceneHandler SceneHandler;
    public Slider masterVol, musicVol, SFXVol;
    public AudioMixer mainAudioMixer;
    
    public void Start()
    {
        LoadVolumeSettings();
    }

    public void ClickedReturn()
    {
        SaveVolumeSettings();
        SceneHandler.DisplayMainMenu(Settings);
    }

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("Master Volume", masterVol.value);  
    }

    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("Music Volume", musicVol.value);  
    }

    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFX", SFXVol.value);  
    }

    public void SaveVolumeSettings()
    {
        //save volume settings so it carries throughout the whole game workflow
        PlayerPrefs.SetFloat("MasterVolume", masterVol.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVol.value);
        PlayerPrefs.SetFloat("SFXVolume", SFXVol.value);
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        // Load the saved volume values, defaulting to 0 if not set
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);

        // Set the slider values
        masterVol.value = masterVolume;
        musicVol.value = musicVolume;
        SFXVol.value = sfxVolume;

        // Set the audio mixer values
        mainAudioMixer.SetFloat("Master Volume", masterVolume);
        mainAudioMixer.SetFloat("Music Volume", musicVolume);
        mainAudioMixer.SetFloat("SFX", sfxVolume);
    }
}
