using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Respawn : MonoBehaviour
{
    public Transform spawnPoint; //référence au point de spawn pour pouvoir apparaître à cet endroit

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            col.transform.position = spawnPoint.position;
        }

    }
            /*bool gameEnded = false;
            public float restartDelay = 1f;

            void OnCollisionEnter2D(Collision2D col)
            {
                if(gameEnded == false)
                {
                    gameEnded = true;
                    Invoke("restart", restartDelay);
                }
            }
            public void restart()
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }*/
        }
