using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    public GameObject Fig;
    public GameObject FigGhost;

    //private GhostCallOut GG = new GhostCallOut ();
    
    void Start()
    {
        
    }
    void Move(float way)
    {
        Vector2 positionOffset = new Vector2(way, 0);
        Vector2 originalPosition = Fig.transform.position;
        Quaternion originalRotation = Fig.transform.rotation;
        Vector2 newPosition = originalPosition + positionOffset;
        GameObject Cool = Instantiate(FigGhost, newPosition, originalRotation);
        
        Debug.Log(Cool.GetComponent< GhostCallOut >().He);
        if (Cool.GetComponent<GhostCallOut>().He)
        {
            Fig.transform.position = newPosition;
            Destroy(Cool);
        }
        
            Destroy(Cool);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            float Call = Input.GetAxisRaw("Horizontal");
            if (Call != 0)
            {
                Move(Call);
            }
        }

        if (Input.GetKeyDown(KeyCode.S)){
            Fig.transform.Rotate(new Vector3(0f,0f,1), 90 , Space.Self);
        }
    }
}
