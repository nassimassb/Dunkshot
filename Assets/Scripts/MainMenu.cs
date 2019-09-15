using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class MainMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void PlayGame()
    {
        Respawn.gameEnded = false; // Pour éviter toute erreur on met gameEnded à false (sinon il risque d'y avoir une erreur avec le menu pause)
        Time.timeScale = 1f; // Pour éviter de démarrer dans un jeu figé on met le temps à la normale 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // passe à la scène suivante
    }


    public void SetVolume(float volume)
    {
        //Debug.Log(volume);
        audioMixer.SetFloat("Volume", volume); //controle le volume
    }
}
