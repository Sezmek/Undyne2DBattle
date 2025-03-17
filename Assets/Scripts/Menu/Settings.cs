using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    private float volume = 0.5f;
    private bool tutorial = true;
    private int difficulty = 2;

    [Header("UI References")]
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown qualityDropdown;
    public TMPro.TMP_Dropdown DifficultyDropdown;
    public Toggle tutorialToggle;
    public Toggle fullscreenToggle;
    public Slider volumeSlider;
    private Resolution[] resolutions;
    private int currentResolutionIndex = 0;

    private void Awake()
    {
        LoadSettings();
        SetSettings();
    }


    private void Start()
    {
        InitializeResolutions();
    }

    private void InitializeResolutions()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }


        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        SetResolution(resolutionDropdown.value);
    }
    public void SetResolution(int resolutionIndex)
    {
        currentResolutionIndex = resolutionIndex;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        SaveSettings();
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        SaveSettings();
    }
    public void SetDifficulty(int difficultyIndex)
    {
        difficulty = difficultyIndex;
        SaveSettings();
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        SaveSettings();
    }

    public float GetVolume()
    {
        return volume;
    }

    public void SetTutorial(bool state)
    {
        tutorial = state;
        SaveSettings();
    }

    public bool GetTutorial()
    {
        return tutorial;
    }

    public void SetQuality(int Qindex)
    {
        QualitySettings.SetQualityLevel(Qindex);
        SaveSettings();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("Difficulty", difficulty);
        PlayerPrefs.SetInt("Quality", QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("Tutorial", tutorial ? 1 : 0);
        PlayerPrefs.SetInt("Fullscreen", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.SetInt("ResolutionIndex", currentResolutionIndex);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("Difficulty"))
            difficulty = PlayerPrefs.GetInt("Difficulty");
        if (PlayerPrefs.HasKey("Quality"))
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        if (PlayerPrefs.HasKey("Volume"))
            volume = PlayerPrefs.GetFloat("Volume");
        if (PlayerPrefs.HasKey("Tutorial"))
            tutorial = PlayerPrefs.GetInt("Tutorial") == 1;
        if (PlayerPrefs.HasKey("Fullscreen"))
            Screen.fullScreen = PlayerPrefs.GetInt("Fullscreen") == 1;
        if (PlayerPrefs.HasKey("ResolutionIndex"))
            currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
    }
    private void SetSettings()
    {
        DifficultyDropdown.value = difficulty;
        tutorialToggle.isOn = tutorial;
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen") == 1;
        volumeSlider.value = volume;
        int savedQualityIndex = PlayerPrefs.HasKey("Quality") ? PlayerPrefs.GetInt("Quality") : QualitySettings.GetQualityLevel();
        qualityDropdown.value = savedQualityIndex;
        qualityDropdown.RefreshShownValue();
    }
}
