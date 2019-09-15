using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour //script servant uniquement pour l'affichage du nombre
{
    public static int scoreValue = 0; //le nombre qui augmentera à chaque panier
    public Text scoreText; //le texte du score dans le jeu

    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 0;
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreValue.ToString();
    }

}
