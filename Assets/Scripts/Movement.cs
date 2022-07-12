using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class Movement : MonoBehaviour
{
    public LayerMask Mask;
    public GameObject Fig;
    Collider2D FigCollider;
    public bool Active = false;
    Rigidbody2D rb;
    KillFather KillF;
    public bool MoveLeft, MoveRight, InQueue = true;
    MakeThemHard Hard;
    public GameObject GameManage;
    SpawnFigure SpawnFig;
    void Start()
    {
        Hard = Fig.GetComponent<MakeThemHard>();
        rb = Fig.GetComponent<Rigidbody2D>();
        FigCollider = Fig.GetComponent<Collider2D>();
        KillF = Fig.GetComponent<KillFather>();
        GameManage = GameObject.Find("GameManage");
        SpawnFig = GameManage.GetComponent<SpawnFigure>();
    }
    void Move(float way)
    {
        Fig.transform.Translate(new Vector3(way, 0, 0),Space.World);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (rb.velocity.y > -0.05f && Active && !InQueue)
        {
            Active = false;
            SpawnFig.CoolFunc(Fig);
            /*StartCoroutine(SpawnFig.ReCreateFigure(Fig));*/
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "RightWall")
        {
            MoveRight = false;
            Debug.Log("Тутовый");
        }
        if (collision.transform.name == "LeftWall")
        {
            MoveLeft = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.name == "RightWall")
        {
            MoveRight = true;
        }
        if (collision.transform.name == "LeftWall")
        {
            MoveLeft = true;
        }
    }

    void Update()
    {
        /*Debug.Log(FigCollider.GetContacts);*/
        if (Active)
        {
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                float Call = Input.GetAxisRaw("Horizontal");
                if (Hard.CouldI("left"))
                {
                    
                    Move(Call);
                }
                
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                float Call = Input.GetAxisRaw("Horizontal");
                if (Hard.CouldI("right"))
                {
                    
                    Move(Call);
                }
            }



            if (Input.GetKeyDown(KeyCode.W))
            {
                Transform ghost = Fig.transform.GetChild(0);
                Collider2D gh = ghost.GetComponent<Collider2D>();
                if (gh.IsTouchingLayers(Mask))
                {
                    Debug.Log("He");
                }
                else
                {
                    Fig.transform.Rotate(new Vector3(0f, 0f, 1), 90, Space.Self);
                }
            }
        }
    }
}
