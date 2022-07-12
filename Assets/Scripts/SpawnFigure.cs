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
    public List<GameObject> Queue;
    GameObject SpawnedFigure,GameFieldFigure,StoredFigure, bufferFigure;
    private void Start()
    {
        KillFather = KillF.GetComponent<KillFather>();
        StartCoroutine("CoroutineExample");
    }
    public void Spawn(int number)
    {
        for(int t = 0; t < number; t++)
        {
            if (IsGameOn && Queue.Count < 4)
            {
                int i = Random.Range(0, Fig.Length);
                SpawnedFigure = Instantiate(Fig[i], new Vector3(14f, 6f, 0f), Quaternion.identity);
                SpawnedFigure.GetComponent<Rigidbody2D>().isKinematic = true;
                SpawnedFigure.GetComponent<ColorMe>().LetsColor();
                Queue.Add(SpawnedFigure);
                foreach(var f in Queue)
                {
                    f.transform.position += new Vector3(0f, 3f, 0f);
                }
            }
        }
    }

    public void Turn()
    {
        if (!IsGameOn) return;
        var FigureToMove = Queue[0];
        var status = FigureToMove.GetComponent<Movement>();
        status.enabled = true;
        FigureToMove.transform.position = new Vector3(4f, 21f, 0f);
        FigureToMove.GetComponent<Rigidbody2D>().isKinematic = false;
        status.InQueue = false;
        status.Active = true;
        GameFieldFigure = FigureToMove;
        Queue.Remove(FigureToMove);
        Spawn(1);
    }
    IEnumerator CoroutineExample()
    {
        yield return new WaitForSeconds(1);
        print("1");
        Spawn(1);
        yield return new WaitForSeconds(1);
        print("2");
        Spawn(1);
        yield return new WaitForSeconds(1);
        print("3");
        Spawn(1);
        yield return new WaitForSeconds(1);
        print("4");
        Turn();
        /*StartCoroutine("CoroutineExample");*/
    }
    public IEnumerator ReCreateFigure(GameObject Figo)
    {

        KillFather.Kill(Figo);
        Debug.Log("оепбши");
        yield return new WaitForSeconds(1);
        Debug.Log("брнпни");
        Spawn(1);

    }
    public void CoolFunc(GameObject Fig)
    {
        Debug.Log("оепбши");
        KillFather.Kill(Fig);
        Turn();
        Debug.Log("брнпни");
    }

    void SwapFigures()
    {
        GameFieldFigure.transform.position = new Vector3(-8f,15f,0f);
        GameFieldFigure.GetComponent<Rigidbody2D>().isKinematic = true;
        GameFieldFigure.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        var cashComponent = GameFieldFigure.GetComponent<Movement>();
        cashComponent.InQueue = true;
        cashComponent.Active = false;
        cashComponent.enabled = false;
        if (!StoredFigure)
        {
            StoredFigure = GameFieldFigure;
            Turn();
        }
        else
        {
            (GameFieldFigure, StoredFigure) = (StoredFigure, GameFieldFigure);
            cashComponent = GameFieldFigure.GetComponent<Movement>();
            GameFieldFigure.transform.position = new Vector3(4f, 21f, 0f);
            GameFieldFigure.GetComponent<Rigidbody2D>().isKinematic = false;
            cashComponent.enabled = true;
            cashComponent.InQueue = false;
            cashComponent.Active = true;
        }

    }

    void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.W))
        {
            Spawn();
        }*/
        if (Input.GetKeyDown(KeyCode.R) && !IsGameOn)
        {
            SceneManager.LoadScene("PhisicOne");
        }
        if(Input.GetKeyUp(KeyCode.S )&& IsGameOn)
        {
            GameFieldFigure.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -3);
        }
        if (Input.GetKeyDown(KeyCode.S) && IsGameOn)
        {
            GameFieldFigure.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -6);
        }
        if (Input.GetKeyDown(KeyCode.F) && IsGameOn)
        {
            SwapFigures();
        }
    }
}
