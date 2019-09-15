using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMove : MonoBehaviour
{

    private KeyCode gauche = KeyCode.LeftArrow;
    private KeyCode droite = KeyCode.RightArrow;
    private KeyCode haut = KeyCode.UpArrow;
    private KeyCode bas = KeyCode.DownArrow;

    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        dest = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);


        if (Input.GetKey(haut))
            dest = (Vector2)transform.position + Vector2.up;
        if (Input.GetKey(droite))
            dest = (Vector2)transform.position + Vector2.right;
        if (Input.GetKey(bas))
            dest = (Vector2)transform.position - Vector2.up;
        if (Input.GetKey(gauche))
            dest = (Vector2)transform.position - Vector2.right;

        //Animation
        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);

    }


}
