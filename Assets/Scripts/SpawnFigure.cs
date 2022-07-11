using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnFigure : MonoBehaviour
{
    public GameObject KillF;
    public bool IsGameOn = true;
    public GameObject[] Fig;
    KillFather KillFather;
    private void Start()
    {
        KillFather = KillF.GetComponent<KillFather>();
        StartCoroutine("CoroutineExample");
    }
    public void Spawn()
    {
        if (IsGameOn)
        {
            int i = Random.Range(0, Fig.Length);
            Instantiate(Fig[i], new Vector3(5f, 20f, 0f), Quaternion.identity);
        }
    }
    IEnumerator CoroutineExample()
    {
        yield return new WaitForSeconds(1);
        print("1");
        yield return new WaitForSeconds(1);
        print("2");
        yield return new WaitForSeconds(1);
        print("3");
        /*StartCoroutine("CoroutineExample");*/
    }
    public IEnumerator ReCreateFigure(GameObject Fig)
    {

        KillFather.Kill(Fig);
        Debug.Log("оепбши");
        yield return new WaitForSeconds(1);
        Debug.Log("брнпни");
        Spawn();

    }
    public void CoolFunc(GameObject Fig)
    {
        Debug.Log("оепбши");
        KillFather.Kill(Fig);
        Spawn();
        Debug.Log("брнпни");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Spawn();
        }
        if (Input.GetKeyDown(KeyCode.R) && !IsGameOn)
        {
            SceneManager.LoadScene("PhisicOne");
        }
    }
}
