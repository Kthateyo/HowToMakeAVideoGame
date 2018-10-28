using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;

public class SettingsMenu : MonoBehaviour {

    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider EffectSlider;
    public Toggle IsFullscreenToggle;
    public Dropdown QualityDropdown;

    public AudioMixer audioMixer;

    Resolution[] resolutions;


    public Dropdown resolutionDropdown;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width.ToString() + " x " + resolutions[i].height.ToString() + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    public void SetMasterVolume(float volume)
    {
        if (volume == -80)
            audioMixer.SetFloat("Master", -80);
        else 
            audioMixer.SetFloat("Master", volume / 4);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume == -80)
            audioMixer.SetFloat("Music", -80);
        else
            audioMixer.SetFloat("Music", volume / 4);
    }

    public void SetEffectVolume(float volume)
    {
        if (volume == -80)
            audioMixer.SetFloat("Effect", -80);
        else
            audioMixer.SetFloat("Effect", volume / 4);
    }



    public void SetGraphicsQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
    }

    private class VolumeSettings
    {
        public float masterVolume;
        public float musicVolume;
        public float effectVolume;
    }

    public void SaveVolumeSettings()
    {
        VolumeSettings volumeSettings = new VolumeSettings();
        volumeSettings.masterVolume = MasterSlider.value;
        volumeSettings.musicVolume = MusicSlider.value;
        volumeSettings.effectVolume = EffectSlider.value;
        
        string json = JsonUtility.ToJson(volumeSettings);
        Debug.Log(json);

        File.WriteAllText(Application.dataPath + "/volumeSettings.json", json);
    }

    public void LoadSettingFromJSON()
    {
        string json = File.ReadAllText(Application.dataPath + "/volumeSettings.json");
        VolumeSettings loadedVolumeSettings = JsonUtility.FromJson<VolumeSettings>(json);

        SetMasterVolume(loadedVolumeSettings.masterVolume);
        MasterSlider.value = loadedVolumeSettings.masterVolume;

        SetMusicVolume(loadedVolumeSettings.musicVolume);
        MusicSlider.value = loadedVolumeSettings.musicVolume;

        SetEffectVolume(loadedVolumeSettings.effectVolume);
        EffectSlider.value = loadedVolumeSettings.effectVolume;
    }

    private class GraphicsSettings
    {
        public int qualitySettings;
        public bool isFullscreen;
        public int resolutionWidth;
        public int resolutionHeight;
        public int refreshRate;
    }

    public void SaveGraphicsSettings()
    {
        Debug.Log("Graphics Settings Saving Started");

        GraphicsSettings graphicsSettings = new GraphicsSettings();
        graphicsSettings.qualitySettings = QualitySettings.GetQualityLevel();
        graphicsSettings.isFullscreen = Screen.fullScreen;
        graphicsSettings.resolutionWidth = Screen.width;
        graphicsSettings.resolutionHeight = Screen.height;
        graphicsSettings.refreshRate = Screen.currentResolution.refreshRate;

        string json = JsonUtility.ToJson(graphicsSettings);
        Debug.Log(json);

        File.WriteAllText(Application.dataPath + "/GraphicsSettings.json", json);
    }

    public void LoadGraphicsSettingsFromJSON()
    {
        if (File.Exists(Application.dataPath + "/GraphicsSettings.json"))
        {
            string json = File.ReadAllText(Application.dataPath + "/GraphicsSettings.json");
            GraphicsSettings graphicsSettings = JsonUtility.FromJson<GraphicsSettings>(json);

            SetGraphicsQuality(graphicsSettings.qualitySettings);
            QualityDropdown.value = graphicsSettings.qualitySettings;
            SetFullscreen(graphicsSettings.isFullscreen);
            IsFullscreenToggle.isOn = graphicsSettings.isFullscreen;
            Screen.SetResolution(graphicsSettings.resolutionWidth, graphicsSettings.resolutionHeight, graphicsSettings.isFullscreen, graphicsSettings.refreshRate);
        }else
        {
            QualityDropdown.value = QualitySettings.GetQualityLevel();
            IsFullscreenToggle.isOn = Screen.fullScreen;
        }
    }
}
