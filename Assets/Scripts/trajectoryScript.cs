using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class trajectoryScript : MonoBehaviour {

	public Sprite dotSprite;                    //Pour changer le sprite des points de trajectoire
    public bool changeSpriteAfterStart;
    public float initialDotSize;                //La taille initiale des points de trajectoire
	public int numberOfDots;                    //Le nombre de points servant à la trajectoire de la balle
    public float dotSeparation;                 //L'espace entre les points de trajectoire 
    public float dotShift;                      //La distance entre le premier point et la balle 
    public float idleTime;                      //Le temps que le joueur devra attendre pour voir l'animation de tutoriel
    private GameObject trajectoryDots;          //L'objet qui stock tous les points de trajectoire
    private GameObject ball;
    private Rigidbody2D ballRB;                 //Le rigidBody lié à la balle
    private Vector3 ballPos;                    //Position de la balle
    private Vector3 fingerPos;                  //Position de l'endroit cliqué avec le doigt/la souris sur l'écran
    private Vector3 ballFingerDiff;             //La distance entre l'endroit cliqué et la position de la balle
    private Vector2 shotForce;                  //La vitesse/velocité de la balle
    private float x1, y1;                       //Position x et y qui va être appliquée à chaque point de trajectoire
    private GameObject helpGesture;             //L'animation d'aide qui va s'activer pour montrer le mouvement à effectuer
    private float idleTimer = 7f;               //Le temps d'inactivité avant que l'animation d'aide apparaisse
    private bool ballIsClicked = false;         //Si le curseur survole la zone cliquable de la balle
    private bool ballIsClicked2 = false;        //Si le doigt/la souris clique sur la balle pour activer le lancer
    private GameObject ballClick;               //La zone sur laquelle le joueur doit cliquer pour activer le lancer
    public float shootingPowerX;                //La puissance qui va être appliquée dans la direction X 
    public float shootingPowerY;                //La puissance qui va être appliquée dans la direction y
    public bool usingHelpGesture;               //Si on veut utiliser le Help Gesture(animation d'aide)
    public bool explodeEnabled;                 //Si on veut effectuer une action lorsque la balle touche un autre objet
    public bool grabWhileMoving;                //Si on veut pouvoir bouger la balle lorsqu'elle est dans les airs
    public GameObject[] dots;                   //Le tableau de points qui servira à la trajectoire
    public bool mask;
    private BoxCollider2D[] dotColliders;

    void Start ()
    {
		ball = gameObject;											
		ballClick = GameObject.Find ("Ball Click Area");            //LA BALL CLICK AREA DEVRA AVOIR LE MEME NOM DANS LA HIERARCHIE QU'ICI SINON LE LANCER NE SERA PAS POSSIBLE ET D'AUTRES PROBL7MES POURRAIENT APPARAITRE
        trajectoryDots = GameObject.Find ("Trajectory Dots");       //LES POINTS DE TRAJECTOIRES DEVRONT AVOIR LE MEME NOM DANS LA HIERARCHIE QU'ICI

        if (usingHelpGesture == true)                               //Si on utilise le Help Gesture
        {								
			helpGesture = GameObject.Find ("Help Gesture");         //LE HELP GESTURE DEVRA AVOIR LE MEME NOM DANS LA HIERARCHIE QU'ICI SI ON MET LE HELP GESTURE A TRUE
        }

		ballRB = GetComponent<Rigidbody2D> ();                      //Le Rigidbody2D de la balle est appliqué au ballRB

        trajectoryDots.transform.localScale = new Vector3 (initialDotSize, initialDotSize, trajectoryDots.transform.localScale.z); //La taille initiale des points de trajectoire est appliquée

        for (int k = 0; k < 40; k++)
        {
			dots [k] = GameObject.Find ("Dot (" + k + ")");         //Tous les points sont appliqués à la position correspondante dans le tableau de points

            if (dotSprite != null)                                  //Si un sprite est appliqué au sprite de points
            {								
				dots [k].GetComponent<SpriteRenderer> ().sprite = dotSprite;    //Tous les points auront ce même sprite
            }
		}
		for (int k = numberOfDots; k < 40; k++)                     //Si le nombre de points utilisés est inférieur à 40, qui est le maximum
        {					
			GameObject.Find ("Dot (" + k + ")").SetActive (false);  //Ils seront cachés
        }

		trajectoryDots.SetActive (false);                            //L'initialisation de la trajectory a été achevée, la trajectoire sera cachée


    }
	

		

	void Update ()
    {

		if (numberOfDots > 40)
        {
			numberOfDots = 40;
		}

		if (usingHelpGesture == true) //Si on utilise le Help Gesture
        {								
			helpGesture.transform.position = new Vector3 (ballPos.x, ballPos.y, ballPos.z); //Le help gesture aura la même position que la balle
        }

		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);  //Utilisé pour déterminer si le doigt/la souris est sur la Ball Click Area

        if (hit.collider != null && ballIsClicked2 == false) //Si la position du curseur est à un endroit mais que le joueur n'a pas cliqué
        {					
			if (hit.collider.gameObject.name == ballClick.gameObject.name) //Si le nom de l'objet sur lequel le joueur est en train de cliquer est le même le nom de la balle
            {   
                ballIsClicked = true;											
			}
            else
            {															
				ballIsClicked = false;                                          //Si ce n'est pas le cas il n'activera pas le lancer
            }
		}
        else
        {																
			ballIsClicked = false;												
		}

		if (ballIsClicked2 == true)
        {                                                                       //Si le lancer est déjà activé
            ballIsClicked = true;                                               //Garder le lancer activé pour plus tard
        }


		if ((ballRB.velocity.x * ballRB.velocity.x) + (ballRB.velocity.y * ballRB.velocity.y) <= 0.0085f) //Si la balle bouge lentement
        { 
			ballRB.velocity = new Vector2 (0f, 0f);                             //Immobilise la balle/met sa vitesse à 0
            idleTimer -= Time.deltaTime;                                        //Commence le timer pour le Help Gesture
        }
        else
        {																
			trajectoryDots.SetActive (false);                                   //Désactive les points de trajectoire
        }

		if (usingHelpGesture == true && idleTimer <= 0f)                        //Si on utilise le Help Gesture and et que le temps du timer est écoulé
        {						
			helpGesture.GetComponent<Animator> ().SetBool ("Inactive", true);   //Commence l'animation du Help Gesture
        }
	

		ballPos = ball.transform.position;                                      //ballPos reçoit la position de la balle

        if (changeSpriteAfterStart == true)                                     //Si on a autorisé le sprite à changer continuellement
        {									
			for (int k = 0; k < numberOfDots; k++)
            {
				if (dotSprite != null)                                          //Si un sprite est appliqué au dotSprite
                {										
					dots [k].GetComponent<SpriteRenderer> ().sprite = dotSprite;//Changer tous les sprites des points en sprite du dotSprite
                }
			}
		}


		if ((Input.GetKey (KeyCode.Mouse0) && ballIsClicked == true) && ((ballRB.velocity.x == 0f && ballRB.velocity.y == 0f) || (grabWhileMoving == true))) //Si le joueur a activé le lancer		//Lorsqu'on appuye sur la balle
        {	
			ballIsClicked2 = true;												

			if (usingHelpGesture == true)
            {										
				idleTimer = idleTime;                                               //On fait un reset
                helpGesture.GetComponent<Animator> ().SetBool ("Inactive", false);  //Si l'animation est en train de se jouer, elle s'arrêtera
            }

			fingerPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);   //La position du doigt/curseur a été trouvée
            fingerPos.z = 0;                                                    //La position z est initialisée à 0

            if (grabWhileMoving == true)                                        //Si on  a activé le lancer alors que la balle était en train de bouger
            {										
				ballRB.velocity = new Vector2 (0f, 0f);                         //La balle arrête de bouger
                ballRB.isKinematic = true;                                      //La balle n'est plus affectée par d'autres forces (Elle reste au même endroit)
            }

			ballFingerDiff = ballPos - fingerPos;								//La distance entre le doigt/curseur et la balle est trouvée 
			
			
			shotForce = new Vector2 (ballFingerDiff.x * shootingPowerX, ballFingerDiff.y * shootingPowerY); //La vélocité du lancer est trouvée


            if ((Mathf.Sqrt ((ballFingerDiff.x * ballFingerDiff.x) + (ballFingerDiff.y * ballFingerDiff.y)) > (0.4f))) //Si la distance entre le doigt/curseur est assez grande
            { 
				trajectoryDots.SetActive (true);                                //Activer la trajectoire
            }
            else
            {
				trajectoryDots.SetActive (false);                               //Sinon..annuler le lancer

                if (ballRB.isKinematic == true)
                {
					ballRB.isKinematic = false;
				}
			}

			for (int k = 0; k < numberOfDots; k++)                              //On donne la position à chaque point de trajectoire
            {							
				x1 = ballPos.x + shotForce.x * Time.fixedDeltaTime * (dotSeparation * k + dotShift);    //La position x de chaque point est trouvée
                y1 = ballPos.y + shotForce.y * Time.fixedDeltaTime * (dotSeparation * k + dotShift) - (-Physics2D.gravity.y/2f * Time.fixedDeltaTime * Time.fixedDeltaTime * (dotSeparation * k + dotShift) * (dotSeparation * k + dotShift));  //La position y de chaque point est trouvée
                dots [k].transform.position = new Vector3 (x1, y1, dots [k].transform.position.z);  //La position z est appliquée à chaque point
            }
		}


		if (Input.GetKeyUp (KeyCode.Mouse0))                                //Si on fait un clique gauche de la souris
        {								
		
			ballIsClicked2 = false;                                         //On ne peut plus viser

            if (trajectoryDots.activeInHierarchy)                           //Si le joueur était en train de viser
            {							
				if(explodeEnabled == true)                                  //Si le joueur était en train de tirer et que le mode Explode est activé
                {									
				    StartCoroutine(explode ());                             //La coroutine "explode" va se lancer
                }
			    trajectoryDots.SetActive (false);							//La trajectoire sera cachée
				ballRB.velocity = new Vector2 (shotForce.x, shotForce.y);   //La balle aura sa nouvelle vélocité

                if (ballRB.isKinematic == true)
                {							
					ballRB.isKinematic = false;								
			    }
		    }
	    }
    }

	public IEnumerator explode()
    {                                           //La fonction explode
        yield return new WaitForSeconds (Time.fixedDeltaTime * (dotSeparation * (numberOfDots - 1f)));	//Rien ne se passera à moins que le temps que le projectile atteigne son dernier point s'écoule
		Debug.Log ("exploded");


        //Insérez ici ce que vous voulez que le mode explode effectue
    }

    public void collided(GameObject dot)
    {
		for (int k = 0; k < numberOfDots; k++)
        {
			if (dot.name == "Dot (" + k + ")")
            {
				for (int i = k + 1; i < numberOfDots; i++)
                {
					
					dots [i].gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				}

			}

		}
	}
	public void uncollided(GameObject dot)
    {
		for (int k = 0; k < numberOfDots; k++)
        {
			if (dot.name == "Dot (" + k + ")")
            {
				for (int i = k-1; i > 0; i--)
                {
					if (dots [i].gameObject.GetComponent<SpriteRenderer> ().enabled == false)
                    {
						Debug.Log ("nigggssss");
						return;
					}
				}

				if (dots [k].gameObject.GetComponent<SpriteRenderer> ().enabled == false)
                {
					for (int i = k; i > 0; i--)
                    {
						
						dots [i].gameObject.GetComponent<SpriteRenderer> ().enabled = true;

					}

				}
			}

		}
	}
}

