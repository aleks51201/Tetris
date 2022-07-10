using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFather : MonoBehaviour
{
    public LayerMask Mask;
    public GameObject Fig;
    public void Kill()
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Kill();
        }
    }
}
