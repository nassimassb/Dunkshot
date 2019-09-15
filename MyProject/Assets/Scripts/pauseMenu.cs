using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class pauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool SettingsOn = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject pauseButton;
    public GameObject settingsButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
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
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        if (SettingsOn)
        {
            settingsMenuUI.SetActive(false);
            settingsButton.SetActive(false);
        }
        Time.timeScale = 1f;
        GameIsPaused = false;
        SettingsOn = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        settingsMenuUI.SetActive(false);
        settingsButton.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        SettingsOn = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Settings()
    {
        settingsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        settingsButton.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        SettingsOn = true;
    }
}
