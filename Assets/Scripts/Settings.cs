using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    [SerializeField] private float volume = 1.0f;
    [SerializeField] private bool tutorial = true;

    [Header("UI References")]
    public TMPro.TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private int currentResolutionIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings();
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
        PlayerPrefs.SetInt("Quality", QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("Tutorial", tutorial ? 1 : 0);
        PlayerPrefs.SetInt("Fullscreen", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.SetInt("ResolutionIndex", currentResolutionIndex);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
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
}
