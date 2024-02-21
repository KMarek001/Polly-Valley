using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [Header("Audio")]
    [SerializeField]
    private AudioSource music;

    [SerializeField]
    private List<AudioSource> sounds;

    [Header("SettingsElements")]
    [SerializeField]
    private Toggle mute;

    [SerializeField]
    private Slider soundsSlider;

    [SerializeField]
    private Slider musicSlider;

    private void Awake()
    {
        instance = this;
    }

    public void SetSoundsVolume()
    {
        if (mute.isOn)
        {
            foreach (AudioSource sound in sounds)
            {
                sound.volume = 0;
            }
            soundsSlider.interactable = false;
        }
        else
        {
            soundsSlider.interactable = true;
            foreach (AudioSource sound in sounds)
            {
                sound.volume = soundsSlider.value;
            }
        }
    }

    public void SetMusicVolume()
    {
        if (mute.isOn)
        {
            music.volume = 0;
            musicSlider.interactable = false;
        }
        else
        {
            musicSlider.interactable = true;
            music.volume = musicSlider.value;
        }
    }

    public void PauseSounds()
    {
        foreach (AudioSource sound in sounds)
        {
            sound.Pause();
        }
    }

    


}
