using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFather : MonoBehaviour
{
    public GameObject Fig;
    void Kill()
    {
        int childrens = Fig.transform.childCount;
        for (int i = childrens-1; i >= 0; i--)
        {
            var child = Fig.transform.GetChild(i);
            child.GetComponent<Rigidbody2D>().isKinematic = false;
            child.GetComponent<Collider2D>().enabled = true;
            child.transform.parent = null;
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
