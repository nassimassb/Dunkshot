﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class MainMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetVolume(float volume)
    {
        //Debug.Log(volume);
        audioMixer.SetFloat("Volume", volume);
    }
}
