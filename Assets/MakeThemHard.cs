using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeThemHard : MonoBehaviour
{
    ContactPoint2D[] list;
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D i in collision.contacts)
        {
            Debug.Log(i.point);
        }
    }
}
