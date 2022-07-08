using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    public LayerMask Mask;
    public GameObject Fig;
    public GameObject FigGhost;
    Collider2D FigCollider;
    public bool Active = true;
    Rigidbody2D rb;
    KillFather KillF;
    public bool MoveLeft, MoveRight = true;
    void Start()
    {
        rb = Fig.GetComponent<Rigidbody2D>();
        FigCollider = Fig.GetComponent<Collider2D>();
        KillF = Fig.GetComponent<KillFather>();
    }
    void Move(float way)
    {
        Fig.transform.Translate(new Vector3(way, 0, 0),Space.World);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Figure")
        {
            Active = false;
            KillF.Kill();
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
            if (Input.GetKeyDown(KeyCode.A) && MoveLeft)
            {
                float Call = Input.GetAxisRaw("Horizontal");
                Move(Call);
            }
            if (Input.GetKeyDown(KeyCode.D) && MoveRight)
            {
                float Call = Input.GetAxisRaw("Horizontal");
                Move(Call);
            }



            if (Input.GetKeyDown(KeyCode.S))
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
