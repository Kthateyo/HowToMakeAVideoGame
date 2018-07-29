using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {

    public AudioMixer audioMixer;

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetEffectVolume(float volume)
    {
        audioMixer.SetFloat("Effect", volume);
    }
}
