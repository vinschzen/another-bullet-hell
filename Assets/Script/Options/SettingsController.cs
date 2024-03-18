using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;
    void Awake() {
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChange);
        sfxSlider.onValueChanged.AddListener(OnSfxVolumeChange);
        masterSlider.onValueChanged.AddListener(OnMasterVolumeChange);
    }

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f);
        UpdateVolume();
    }

    public void OnMusicVolumeChange(float value)
    {
        audioMixer.SetFloat("MusicVolume", (float) Math.Log10(value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", (float) Math.Log10(value) * 20);
        PlayerPrefs.Save();
    }

    public void OnSfxVolumeChange(float value)
    {
        audioMixer.SetFloat("SfxVolume", (float) Math.Log10(value) * 20 );
        PlayerPrefs.SetFloat("SfxVolume", (float) Math.Log10(value) * 20 );
        PlayerPrefs.Save();
    }

    public void OnMasterVolumeChange(float value)
    {
        audioMixer.SetFloat("MasterVolume", (float) Math.Log10(value) * 20 );
        PlayerPrefs.SetFloat("MasterVolume", (float) Math.Log10(value) * 20 ); 
        PlayerPrefs.Save();
    }

    private void UpdateVolume()
    {
        // Update sliders based on current mixer values (optional)
        // musicSlider.value = audioMixer.GetFloat("MusicVolume", 0f); // Get current volume from mixer
        // sfxSlider.value = audioMixer.GetFloat("SfxVolume", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
