using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseDetection : MonoBehaviour
{
    public GameObject GameManage;
    SpawnFigure SpawnFig;
    private void Start()
    {
        SpawnFig = GameManage.GetComponent<SpawnFigure>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Figure")
        {
            SpawnFig.IsGameOn = false;
        }
    }

}
