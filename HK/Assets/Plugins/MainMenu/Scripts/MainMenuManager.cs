using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public ToggleClass Toggles;
    public MainMenuClass MainMenu;
    public OptionsMenuClass OptionsMenu;
    public LoadingSceneClass LoadingScene;
    public readonly ResolutionValues[] resolutions = {
        new ResolutionValues(1920, 1080),
        new ResolutionValues(1280, 720),
        new ResolutionValues(854, 480),
    };
    private int selectedResolution;
    
    #region VariableClasses
    [Serializable]
    public class ToggleClass
    {
        public bool ShouldShowOption = true;
        public bool ShouldShowGraphicsOptions = true;
        public bool ShouldShowAudioOptions = true;
        public bool ShouldLoopResolutions = true;
    }

    [Serializable]
    public class MainMenuClass
    {
        public GameObject OptionsButton = null;
        public GameObject optionsScreen = null;
        public string levelToLoad = "";
    }

    [Serializable]
    public class OptionsMenuClass
    {
        public GameObject graphicsLabel = null;
        public GameObject graphicsOptions = null;
        public Text resolutionButtonText = null;
        public GameObject graphicsApplyButton = null;
        public GameObject audioLabel = null;
        public GameObject audioOptions = null;
        public Toggle VSyncToggle, FullscreenToggle = null;
        public AudioMixer Mixer;
        public Slider MasterSlider, MusicSlider, SFXSlider;
        public Text MasterLabel, MusicSliderLabel, SFXLabel;
    }
    
    [Serializable]
    public class LoadingSceneClass
    {
        public GameObject loadingScreen;
        public Slider loadingBar;
        public Text loadingText;
    }
    
    [Serializable]
    public class ResolutionValues
    {
        public int Width, Height;

        public ResolutionValues(int w, int h)
        {
            Width = w;
            Height = h;
        }
    }
    #endregion

    #region Init
    // Start is called before the first frame update
    void Start()
    {
        ShowGameObjects();
        InitializeMenus();
        LoadVolumes();
    }

    private void InitializeMenus()
    {
        MainMenu.optionsScreen.SetActive(false);
        
        //Graphics Settings
        OptionsMenu.FullscreenToggle.isOn = Screen.fullScreen;
        if (QualitySettings.vSyncCount == 0)
        {
            OptionsMenu.VSyncToggle.isOn = false;
        }
        else
        {
            OptionsMenu.VSyncToggle.isOn = true;
        }
        
        //Resolution settings
        bool foundResolution = false;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (Screen.width == resolutions[i].Width && Screen.height == resolutions[i].Height)
            {
                foundResolution = true;
                selectedResolution = i;
                ResolutionTextUpdate();
            }
        }

        if (!foundResolution)
        {
            OptionsMenu.resolutionButtonText.text = Screen.width + " X " + Screen.height;
        }
    }

    private void LoadVolumes()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            OptionsMenu.Mixer.SetFloat("MasterVolValue", PlayerPrefs.GetFloat("MasterVolume"));
            OptionsMenu.MasterSlider.value = PlayerPrefs.GetFloat("MasterVolValue");
            OptionsMenu.MasterLabel.text = (OptionsMenu.MasterSlider.value + 80).ToString();
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            OptionsMenu.Mixer.SetFloat("MusicVolValue", PlayerPrefs.GetFloat("MusicVolume"));
            OptionsMenu.MusicSlider.value = PlayerPrefs.GetFloat("MusicVolValue");
            OptionsMenu.MusicSliderLabel.text = (OptionsMenu.MusicSlider.value + 80).ToString();
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            OptionsMenu.Mixer.SetFloat("SFXVolValue", PlayerPrefs.GetFloat("SFXVolume"));
            OptionsMenu.SFXSlider.value = PlayerPrefs.GetFloat("SFXVolValue");
            OptionsMenu.SFXLabel.text = (OptionsMenu.SFXSlider.value + 80).ToString();
        }
    }

    private void ShowGameObjects()
    {
        MainMenu.OptionsButton.SetActive(Toggles.ShouldShowOption);
        OptionsMenu.graphicsLabel.SetActive(Toggles.ShouldShowGraphicsOptions);
        OptionsMenu.graphicsOptions.SetActive(Toggles.ShouldShowGraphicsOptions);
        OptionsMenu.graphicsApplyButton.SetActive(Toggles.ShouldShowGraphicsOptions);
        OptionsMenu.audioLabel.SetActive(Toggles.ShouldShowAudioOptions);
        OptionsMenu.audioOptions.SetActive(Toggles.ShouldShowAudioOptions);
    }
    #endregion

    #region MainMenu
    public void StartGame()
    {
        if (MainMenu.levelToLoad != "")
        {
            StartCoroutine(LoadSceneAsync(MainMenu.levelToLoad));
        }
    }

    public void OpenOptionsMenu()
    {
        if (MainMenu.optionsScreen.activeInHierarchy == false)
        {
            MainMenu.optionsScreen.SetActive(true);
        }
    }

    public void CloseOptions()
    {
        if (MainMenu.optionsScreen.activeInHierarchy)
        {
            MainMenu.optionsScreen.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region OptionsMenu

    #region Graphics
    public void ResolutionLeftButton()
    {
        if (Toggles.ShouldLoopResolutions)
        {
            if ((selectedResolution - 1) == 0 )
            {
                selectedResolution = resolutions.Length - 1;
            }
            else
            {
                selectedResolution--;
            }
        }
        else
        {
            selectedResolution--;
            if (selectedResolution < 0 )
            {
                selectedResolution = 0;
            }
        }

        ResolutionTextUpdate();
    }
    
    public void ResolutionRightButton()
    {
        if (Toggles.ShouldLoopResolutions)
        {
            if ((selectedResolution + 1) == resolutions.Length - 1)
            {
                selectedResolution = 0;
            }
            else
            {
                selectedResolution++;
            }
        }
        else
        {
            selectedResolution++;
            if (selectedResolution > resolutions.Length - 1)
            {
                selectedResolution = resolutions.Length - 1;
            }
        }
        ResolutionTextUpdate();
    }
    
    private void ResolutionTextUpdate()
    {
        OptionsMenu.resolutionButtonText.text = resolutions[selectedResolution].Width + " X " +
                                                 resolutions[selectedResolution].Height;
    }
    
    public void ApplyGraphics()
    {
        //Apply VSync
        if (OptionsMenu.VSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        
        //Apply Resolution
        Screen.SetResolution(
            resolutions[selectedResolution].Width,
            resolutions[selectedResolution].Height,
                OptionsMenu.FullscreenToggle.isOn
            );
    }
    #endregion

    #region Audio

    public void SetMasterVolume()
    {
        OptionsMenu.MasterLabel.text = (OptionsMenu.MasterSlider.value + 80).ToString();
        OptionsMenu.Mixer.SetFloat("MasterVolValue", OptionsMenu.MasterSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", OptionsMenu.MasterSlider.value);
    }
    public void SetMusicVolume()
    {
        //OptionsMenu.MusicSlider.maxValue = 0; 
        OptionsMenu.MusicSliderLabel.text = (OptionsMenu.MusicSlider.value + 80).ToString();
        OptionsMenu.Mixer.SetFloat("MusicVolValue", OptionsMenu.MusicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", OptionsMenu.MusicSlider.value);
    }
    public void SetSFXVolume()
    {
        OptionsMenu.SFXLabel.text = (OptionsMenu.SFXSlider.value + 80).ToString();
        OptionsMenu.Mixer.SetFloat("SFXVolValue", OptionsMenu.SFXSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", OptionsMenu.SFXSlider.value);
    }

    #endregion
    #endregion

    #region Loading
    IEnumerator LoadSceneAsync(string scene)
    {
        LoadingScene.loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                LoadingScene.loadingText.text = "Press any key to continue...";
                LoadingScene.loadingBar.value = 1.0f;
                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                    Time.timeScale = 1.0f;
                }
            }
            else
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                LoadingScene.loadingBar.value = progress;
            }
            yield return null;
        }
    }
    

    #endregion
}

