using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public Transform[] pointsdedeplacement;
    int actu = 0;
    public float vitesse = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //on cherche le point suivant
        if (transform.position != pointsdedeplacement[actu].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, pointsdedeplacement[actu].position, vitesse);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }

        //Si le point est atteint alors on recherche le point suivant
        else
            actu = (actu + 1) % pointsdedeplacement.Length;

        //pour l'anim
        Vector2 dir = pointsdedeplacement[actu].position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    void OnTriggerEnter2D(Collider2D ghost) //si un fantome touche pacman alors pacman disparait
    {
        if (ghost.name == "Pacman")
        {
            Destroy(ghost.gameObject); 
            
            //Perdu//
            //
            //
            //
            //
            //
            //
        }
            
    }
}
