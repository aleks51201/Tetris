using UnityEngine;


public class Cell : MonoBehaviour
{
    private GameObject childCell;

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
        this.childCell.GetComponent<Rigidbody2D>().isKinematic = isKinematic;
    }

    public void IsColider2DEnaled(bool isEnabled)
    {
        this.childCell.GetComponent<Collider2D>().enabled = isEnabled;
    }

    public void SetLayer(int nummerateLayer)
    {
        this.childCell.layer = nummerateLayer;
    }

    public void DestoyCell()
    {
        Destroy(this.childCell);
    }

    private void ChangeLayerDependingOnVelocity()
    {
        bool cellKinematic = this.childCell.GetComponent<Rigidbody2D>().isKinematic;
        if (!cellKinematic)
        {
            Vector2 cellVelocity = this.childCell.GetComponent<Rigidbody2D>().velocity;
            if (cellVelocity.y < -0.5f)
                this.childCell.layer = LayerMask.NameToLayer("Neon");
            else if (cellVelocity.y >= -0.5f)
                this.childCell.layer = LayerMask.NameToLayer("Detection");
        }
    }

    private void Start()
    {
        childCell = this.gameObject;
    }

    private void OnEnable()
    {
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
    }

    private void OnDisable()
    {
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
    }

    private void FixedUpdate()
    {
        ChangeLayerDependingOnVelocity();
    }
}
