using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFather : MonoBehaviour
{
    SpawnFigure SpawnFig;

    private void Start()
    {
    }
    public void Kill(GameObject Fig)
    {
        int childrens = Fig.transform.childCount;
        for (int i = childrens - 1; i >= 0; i--)
        {
            var child = Fig.transform.GetChild(i);
            if (child.name == "Ghost")
            {
                Destroy(child.gameObject);
            }
            else
            {
                child.GetComponent<Rigidbody2D>().isKinematic = false;
                child.GetComponent<Collider2D>().enabled = true;
                child.GetComponent<Collider2D>().enabled = true;
                child.gameObject.layer = 6;
                
                child.transform.parent = null;
            }

        }
        Destroy(Fig);
        
    }
    

}
