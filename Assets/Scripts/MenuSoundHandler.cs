using UnityEngine;
using UnityEngine.Audio;

public class MenuSoundHandler : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip purchaseSFX;
    public AudioClip equipSFX;
    public AudioClip buttonPressSFX;
    public AudioClip errorSFX;
    public AudioClip gameMusic;

    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup;
    public AudioMixer mainAudioMixer;

    void Start()
    {
        // Assign the AudioMixerGroups to the AudioSources
        musicSource.outputAudioMixerGroup = musicMixerGroup;
        sfxSource.outputAudioMixerGroup = sfxMixerGroup;

        // Load and apply volume settings
        LoadVolumeSettings();
        PlayGameMusic();
    }

    public void PlayPurchaseSFX()
    {
        PlaySFX(purchaseSFX);
    }

    public void PlayEquipSFX()
    {
        PlaySFX(equipSFX);
    }

    public void PlayButtonPressSFX()
    {
        PlaySFX(buttonPressSFX);
    }

    public void PlayErrorSFX()
    {
        PlaySFX(errorSFX);
    }

    public void PlayGameMusic()
    {
        musicSource.clip = gameMusic;
        musicSource.loop = true;
        musicSource.Play(); 
    }

    public void StopGameMusic()
    {
        musicSource.Stop();
    }

    private void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    private void LoadVolumeSettings()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);

        mainAudioMixer.SetFloat("Master Volume", masterVolume);
        mainAudioMixer.SetFloat("Music Volume", musicVolume);
        mainAudioMixer.SetFloat("SFX", sfxVolume);
    }

    public void SetMasterVolume(float volume)
    {
        mainAudioMixer.SetFloat("Master Volume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        mainAudioMixer.SetFloat("Music Volume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        mainAudioMixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
}