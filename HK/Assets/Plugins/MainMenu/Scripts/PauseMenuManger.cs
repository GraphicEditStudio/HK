using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManger : MonoBehaviour
{
    [SerializeField] string mainMenuScene;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private Text loadingText;
    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.SetActive(false);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        //LoadLevel(1);
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }
    }

    public void PauseUnPause()
    {
        if (!isPaused)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0.0f;
        }
        else if (isPaused)
        {
            pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1.0f;
        }
    }
    
    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1.0f;
        //SceneManager.LoadScene(mainMenuScene);
        StartCoroutine(LoadSceneAsync(mainMenuScene));
    }
    
    IEnumerator LoadSceneAsync(string scene)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                loadingText.text = "Press any key to continue...";
                loadingBar.value = 1.0f;
                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                    Time.timeScale = 1.0f;
                }
            }
            else
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                loadingBar.value = progress;
            }
            yield return null;
        }
    }
}
