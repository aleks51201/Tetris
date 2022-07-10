using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeThemHard : MonoBehaviour
{
    ContactPoint2D[] list;
    public GameObject Batya;
    List <Vector3> Positions = new();
    public List<GameObject> BlockMoveRight = new();
    public List<GameObject> BlockMoveLeft = new();

    void Start()
    {
    }
    List<Vector3> GetCoordinates()
    {
        var detki = Batya.GetComponentsInChildren<Transform>();
        foreach(var i in detki)
        {
            if (i.name != "Ghost" && i.name != Batya.transform.name)
            {
                Positions.Add(i.position);
            }          
        }
        return Positions;


    }
/*    private void OnCollisionEnter2D(Collision2D collision)
    {
        List < Vector3 > PosForCheck = GetCoordinates();
        foreach (ContactPoint2D i in collision.contacts)
        {
            foreach(Vector3 y in PosForCheck)
            {
                if(y.x < i.point.x + 0.2f ){
                    BlockMoveRight.Add(collision.gameObject);
                }
                else if (y.x > i.point.x - 0.2f)
                {
                    BlockMoveLeft.Add(collision.gameObject);
                }
            }
        }
        Positions.Clear();
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        List<Vector3> PosForCheck = GetCoordinates();
        Vector2 i = collision.transform.position;
            foreach (Vector3 y in PosForCheck)
            {
                if (y.x < i.x)
                {
                    BlockMoveRight.Add(collision.gameObject);
                }
                else if (y.x > i.x)
                {
                    BlockMoveLeft.Add(collision.gameObject);
                }
            }
        
        Positions.Clear();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(BlockMoveRight.Count);
        int b = BlockMoveRight.RemoveAll(i => i == collision.gameObject);
        BlockMoveLeft.RemoveAll(i => i == collision.gameObject);
        Debug.Log(b);
    }
    public bool CouldI(string which)
    {
        if(which == "right")
        {
            if(BlockMoveRight.Count == 0)
            {
                return true;
            }
        }
        if (which == "left")
        {
            if (BlockMoveLeft.Count == 0)
            {
                return true;
            }
        }
        return false;
    }
/*    private void OnCollisionExit2D(Collision2D collision)
    {
        BlockMoveRight.RemoveAll(GameObject => collision.gameObject);
        BlockMoveLeft.RemoveAll(GameObject => collision.gameObject);
    }*/
}
