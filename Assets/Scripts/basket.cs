using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basket : MonoBehaviour
{

    // Start is called before the first frame update
    public int b = 0;
    void Start()
    {
        int a = 3;
        b = 2;

        GameObject prefab = Resources.Load("Basket") as GameObject;

        for (int i = 1; i < 10; i++)
        {
            GameObject go = Instantiate(prefab);

            if (i%2 == 0)
            {
                a -= 6;
            } else
            {
                a += 6;
            }

            b += 4;
           
            go.transform.position = new Vector2(a,b);

        }

      //  }
    }

    // Update is called once per frame
    void Update()
    { 
    }


}
