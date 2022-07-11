using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDetection : MonoBehaviour
{
    public RaycastHit2D[] Hitted;
    Vector3 OriginPlace, CurrentPlace;
    public GameObject RaycatsEmpty;
    int Mask, Points;
    List <RaycastHit2D> HowFull = new () ;
    private void Start()
    {
        OriginPlace = RaycatsEmpty.transform.position;
        CurrentPlace = OriginPlace;
        Mask = LayerMask.GetMask("Detection");
    }
    void FixedUpdate()
    {
        Hitted = Physics2D.RaycastAll(CurrentPlace, Vector2.right, 10, Mask);
        foreach (RaycastHit2D i in Hitted)
        {
            if(i.transform.tag == "Figure")
            {
                HowFull.Add(i);
            } 
        }
        if(HowFull.Count >= 20)
        {
            for(int i = 0; i < HowFull.Count; i++)
            {
                
                Destroy(HowFull[i].transform.gameObject);
            }
            Points += 100;
        }else if(HowFull.Count > 0)
        {
            CurrentPlace = CurrentPlace + new Vector3(0f, 1f, 0f);
        }
        else
        {
            CurrentPlace = OriginPlace;
        }
        HowFull.Clear();

    }
}
