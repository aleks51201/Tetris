using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

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
    MakeThemHard Hard;
    void Start()
    {
        Hard = Fig.GetComponent<MakeThemHard>();
        rb = Fig.GetComponent<Rigidbody2D>();
        FigCollider = Fig.GetComponent<Collider2D>();
        KillF = Fig.GetComponent<KillFather>();
    }
    void Move(float way)
    {
        Fig.transform.Translate(new Vector3(way, 0, 0),Space.World);
        /*yield return new WaitForFixedUpdate();*/
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (rb.velocity.y > -0.05f)
        {
            Active = false;
            KillF.Kill();
        }
        /*        if(collision.transform.tag == "Figure")
                {
                    Active = false;
                    KillF.Kill();
                }*/
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
