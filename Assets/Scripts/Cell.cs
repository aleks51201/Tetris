using System.Collections;
using UnityEngine;


public class Cell : MonoBehaviour
{
    public GameObject childCell;

    private void OnEnable()
    {
       BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
    }

    private void OnDisable()
    {
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
    }
    private void OnDeleteTetromino(GameObject tetromino)
    {
        if (childCell.transform.IsChildOf(tetromino.transform))
        {
            IsRigidBody2DKinematic(false);
            IsColider2DEnaled(true);
            SetLayer(6);
            BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        }
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
