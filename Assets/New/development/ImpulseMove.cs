using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(-1);
           
        };
        if (Input.GetKeyDown(KeyCode.D))
        {
            Move(1);
         
        };
    }
    private void FixedUpdate()
    {
        Debug.Log(transform.position);
    }

    private void Move(int a)//50000 fose
    {

        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000*a,0),ForceMode2D.Impulse);

        Round();
    }

    private void Round()
    {
        Thread.Sleep(100);
        Vector2 currentPosition = transform.position;
        float x1 = 0;
        float y1 = 0;

        x1 =(float) Math.Round( currentPosition.x);
        y1 = currentPosition.y;

        transform.position = new Vector2(x1, y1);

        Debug.Log(transform.position + "ROUND");
        //transform.GetComponent<Collider2D>().IsTouching=true; 

    }
}
