using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonus : MonoBehaviour
{
    public static bool bonusLevel = false;
    public GameObject gameUI;
    public GameObject bonusUI;

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player")) //si le joueur touche le collider2D du panier
        {
            bonusLevel = true;
            gameUI.SetActive(false);
            bonusUI.SetActive(true);
        }

    }
}
