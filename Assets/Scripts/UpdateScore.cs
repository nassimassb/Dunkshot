using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    public Rigidbody2D ballRB; //rigidBody2D de la balle
    bool BasketScored = false; //ce booleen est utilisé pour ne pas marquer 2 fois dans le même panier
    public GameObject deadzone;
    public GameObject pacman; //le jeu pacman montera à chaque fois
    Vector2 tempos; //position temporaire

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player")) //si le joueur touche le collider2D du panier
        {
            ballRB.velocity = new Vector2(0f, 0f); //la vitesse de la balle est réinitialisé à 0;

            if (BasketScored == false)
            {
                Score.scoreValue += 1;
                BasketScored = true;
                FindObjectOfType<AudioManager>().Play("goal");
                //remonter la deadzone lorque le joueur monte
                if (Score.scoreValue % 2 == 0 && Score.scoreValue >= 2)
                {
                    tempos = deadzone.transform.position;
                    tempos.y += 7;
                    deadzone.transform.position = tempos;
                }
            }
        }

    }
}
