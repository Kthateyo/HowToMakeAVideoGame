using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    public Slider music;
    public Slider effects;
        
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
        }
    }

    private void Update()
    {
        foreach (Sound s in sounds)
        {
            if (s.name == "BackgroundMusic")
            {
                s.source.volume = s.volume * music.value;
            }
            else
            {
                s.source.volume = s.volume * effects.value;
            }
        }  
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

}
