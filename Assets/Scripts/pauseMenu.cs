using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class pauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; //sert à savoir si le jeu est en pause
    public static bool SettingsOn = false; //sert à savoir si le menu des options est activé
    public GameObject pauseMenuUI; // c'est simplement notre menu pause 
    public GameObject settingsMenuUI; //menu des options
    public GameObject pauseButton;
    public GameObject settingsButton;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // si on appuye sur la touche Escape
        {
            if (!Respawn.gameEnded) // On ne pourra pas activer le menu pause si on est dans le menu restart
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
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // on désactive le menu pause
        pauseButton.SetActive(true);
        if (SettingsOn)
        {
            settingsMenuUI.SetActive(false);
            settingsButton.SetActive(false);
        }
        Time.timeScale = 1f; // on remet le temps à la normale
        GameIsPaused = false;
        SettingsOn = false;
    }

    public void Pause()
    {
            pauseMenuUI.SetActive(true);
            pauseButton.SetActive(false);
            settingsMenuUI.SetActive(false);
            settingsButton.SetActive(true);
            Time.timeScale = 0f; //stop tout mouvement du jeu et le met donc en pause
            GameIsPaused = true;
            SettingsOn = false;
    }

    public void backToPause()  
    {
        if (Respawn.gameEnded) 
        {
            pauseButton.SetActive(false);
            settingsMenuUI.SetActive(false);
            settingsButton.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            SettingsOn = false;
        }
        else
        {
            Pause();
        }
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // retourne à la scène précédente (au menu principal)
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
