using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;

    public const string MUSIC_VOLUME_PREFS_KEY = "musicVolume";
    public const string MUSIC_GROUP = "MusicVolume";
    public const string SFX_VOLUME_PREFS_KEY = "sfxVolume";
    public const string SFX_GROUP = "SfxVolume";

    private void Start()
    {
        if (PlayerPrefs.HasKey(MUSIC_VOLUME_PREFS_KEY))
        {
            LoadMusicVolume();
        }
        else
        {
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey(SFX_VOLUME_PREFS_KEY))
        {
            LoadSFXVolume();
        }
        else
        {
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = _musicVolumeSlider.value;
        _mixer.SetFloat(MUSIC_GROUP, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_PREFS_KEY, volume);
        // PlayerPrefs.Save();
    }
    
    private void LoadMusicVolume()
    {
        _musicVolumeSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME_PREFS_KEY);
        SetMusicVolume();
    }
    
    public void SetSFXVolume()
    {
        float volume = _sfxVolumeSlider.value;
        _mixer.SetFloat(SFX_GROUP, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SFX_VOLUME_PREFS_KEY, volume);
        // PlayerPrefs.Save();
    }
    
    private void LoadSFXVolume()
    {
        _sfxVolumeSlider.value = PlayerPrefs.GetFloat(SFX_VOLUME_PREFS_KEY);
        SetSFXVolume();
    }
}