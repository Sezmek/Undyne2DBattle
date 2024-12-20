using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    private AudioSource backgroundMusic;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused && pauseMenuUI.activeSelf)
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        backgroundMusic = BattleManager.instance.audioSource;
        if (backgroundMusic != null && BattleManager.instance.hasStartedMusic && !backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }

    }
    public void Restart()
    {
        gameIsPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Pause()
    {
        if (gameIsPaused)
            return;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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

