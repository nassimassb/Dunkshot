using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class Respawn : MonoBehaviour
{
    public static bool gameEnded = false; //si le joueur a perdu
    public Transform spawnPoint; //référence au point de spawn pour pouvoir apparaître à cet endroit
    public Rigidbody2D ballRB; //rigidBody 2D de la balle
    public float restartDelay = 0.2f; //permet de laisser un delai avant de recommencer la scene
    public GameObject restartMenuUI;
    public GameObject pauseButton;
    public GameObject ball;
    public Text highScore;
    public Text highScore1; //High Score dans les différents menus
    public Text highScore2; //High Score dans les différents menus
    public Text highScore3; //High Score dans les différents menus



    private void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString(); //initialise le High Score
        highScore1.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        highScore2.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player")) //si le joueur touche la deadzone
        {
            if (Score.scoreValue == 0)
            {
                FindObjectOfType<AudioManager>().Play("death");
                col.transform.position = spawnPoint.position; //la balle apparait à la position du spawnPoint
                ballRB.velocity = new Vector2(0f, 0f); //la vitesse de la balle est réinitialisé à 0;
                Score.scoreValue = 0; //on réinitialise le score à 0 pour pouvoir recommencer
            }
            else
            {
                gameEnded = true;
                FindObjectOfType<AudioManager>().Play("death");
                restartMenu();
                ball.SetActive(false); //on fait disparaître la balle pour ne pas la voir dans le menu restart

                //Mets à jour le High Score
                if (Score.scoreValue > PlayerPrefs.GetInt("HighScore", 0)) //si le score actuel est plus grand que le High Score
                {
                    PlayerPrefs.SetInt("HighScore", Score.scoreValue); //il deviendra le nouveau High Score
                    highScore.text = Score.scoreValue.ToString();
                    highScore1.text = Score.scoreValue.ToString();
                    highScore2.text = Score.scoreValue.ToString();
                }
            }
        }
    }

    public void restartMenu()
    {
        restartMenuUI.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //permet de recommencer la scène
        gameEnded = false;
    }

    public void resetHighScore() //permet de réinitialiser le High Score
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore.text = "0";
        highScore1.text = "0";
        highScore2.text = "0";

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
