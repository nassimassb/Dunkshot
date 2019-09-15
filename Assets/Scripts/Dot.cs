using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D co)
    {
        int nbrTotaldePoints = 398; //nombre de pacDots(fais manuellement)
        if (co.name == "Pacman")
        {
            nbrTotaldePoints--;
            Destroy(gameObject);
        }

        if(nbrTotaldePoints == 0)
        {
            //Gagné//
            
            

        }
            
    }
}
