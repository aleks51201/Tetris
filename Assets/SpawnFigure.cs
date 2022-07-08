using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFigure : MonoBehaviour
{
    public GameObject[] Fig;
    // Start is called before the first frame update
    void Spawn()
    {
        int i = Random.Range(0, Fig.Length);
        Instantiate(Fig[i], new Vector3(5f, 20f, 0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Spawn();
        }
    }
}
