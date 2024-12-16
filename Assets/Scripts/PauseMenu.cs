using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public AudioSource backgroundMusic;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        backgroundMusic = BattleManager.instance.audioSource;
        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    void Pause()
    {
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        backgroundMusic = BattleManager.instance.audioSource;
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause();
        }
    }

    public void GoToMenu()
    {
        gameIsPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

