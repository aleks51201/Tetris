using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCallOut : MonoBehaviour
{
    private bool he = false;
    
    void Awake()
    {
        Debug.Log("sjrgniourwhoiregn");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("sjrgniourwhoiregn");
        he = true;
        if (collision.gameObject.tag != "Figure")
        {
            
            he = true;
        }
    }
    public bool Hohoho()
    {
        return true;
    }
    public bool He
    {
        get { return he; }
        set { he = value; }
    }
}
