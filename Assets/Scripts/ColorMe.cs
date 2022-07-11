using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMe : MonoBehaviour
{
    public GameObject Fig;


    public void LetsColor()
    {
        int childrens = Fig.transform.childCount;
        int r = Random.Range(0, ColorHolder.Colors.Length);
        for (int i = childrens - 1; i >= 0; i--)
        {
            var child = Fig.transform.GetChild(i);
            if (child.name != "Ghost")
            {
                child.GetComponent<SpriteRenderer>().color = ColorHolder.Colors[r];
            }
        }
    }
}
