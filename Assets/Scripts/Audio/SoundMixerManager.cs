using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource sliderAudioSource;
    [SerializeField] private Slider fxSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        float fxValue;
        float musicValue;
        audioMixer.GetFloat("soundFXVolume", out fxValue);
        audioMixer.GetFloat("musicVolume", out musicValue);
        fxSlider.value = fxValue;
        musicSlider.value = musicValue;
    }
    public void SetSoundFXVolume(float level)
    {
        if (!sliderAudioSource.isPlaying && fxSlider.IsActive())
        {
            AudioManager.instance.PlaySoundFXClip(sliderAudioSource.clip, transform, 1f, 0.45f);
        }
        audioMixer.SetFloat("soundFXVolume", level);

    }
    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", level);

    }
}
