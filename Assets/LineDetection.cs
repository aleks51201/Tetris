using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public RaycastHit2D[] Hitted;
    Vector3 OriginPlace;
    public GameObject RaycatsEmpty;
    int Mask;
    List <RaycastHit2D> HowFull = new () ;
    private void Start()
    {
        OriginPlace = RaycatsEmpty.transform.position;
        Mask = LayerMask.GetMask("Detection");
        Debug.Log(Mask);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Hitted = Physics2D.RaycastAll(OriginPlace, Vector2.right, 10, Mask);
        Debug.DrawRay(OriginPlace, Vector2.right,Color.red,Mathf.Infinity);
        Debug.Log("Объекты " + Hitted.Length);
        foreach (RaycastHit2D i in Hitted)
        {
            Debug.Log("Слои "+i.transform.gameObject.layer);
            if(i.transform.tag == "Figure")
            {
                HowFull.Add(i);
            } 
        }
        if(HowFull.Count >= 10)
        {
            for(int i = 0; i < HowFull.Count; i++)
            {
                
                Destroy(HowFull[i].transform.gameObject);
            }
        }
        Debug.Log("Всего фигур " + HowFull.Count);
        HowFull.Clear();

    }
}
