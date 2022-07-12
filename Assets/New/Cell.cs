using System.Collections;
using UnityEngine;


public class Cell : MonoBehaviour
{
    public GameObject childCell;

    private Figure tetromino;
    private void Start()
    {
        tetromino = childCell.transform.parent.GetComponent<Figure>();
        tetromino.OnDeleteTetrominoEvent += OnDeleteTetromino;
    }
    private void OnDeleteTetromino()
    {
        IsRigidBody2DKinematic(false);
        IsColider2DEnaled(true);
        SetLayer(6);
    }

    public void IsRigidBody2DKinematic(bool isKinematic)
    {
        this.childCell.GetComponent<Rigidbody2D>().isKinematic= isKinematic;
    }
    public void IsColider2DEnaled(bool isEnabled)
    {
        this.childCell.GetComponent<Collider2D>().enabled = isEnabled;
    }
    public void SetLayer(int nummerateLayer)
    {
        this.childCell.layer = nummerateLayer;
    }

}
