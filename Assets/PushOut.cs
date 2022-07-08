using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushOut : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Fig;
    private void Start()
    {
        
    }
/*    private void OnCollisionStay2D(Collision2D collision)
    {

        if(collision.gameObject.name == "LeftWall")
        {
            Vector2 NewPos;
            Vector2 CurrentPos = Fig.transform.position;
            Debug.Log(CurrentPos);
            NewPos = CurrentPos + new Vector2(1, 0);
            Debug.Log(NewPos);
            Fig.transform.position = NewPos;
        }
        if (collision.gameObject.name == "RightWall")
        {
            Vector2 NewPos;
            Vector2 CurrentPos = Fig.transform.position;
            Debug.Log(CurrentPos);
            NewPos = CurrentPos + new Vector2(-1, 0);
            Debug.Log(NewPos);
            Fig.transform.position = NewPos;
        }
    }*/
}
