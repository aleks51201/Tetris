using UnityEngine;


public class Cell : MonoBehaviour
{
    private GameObject childCell;

    public void OnDeleteTetromino(GameObject tetromino)
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
        childCell.GetComponent<Rigidbody2D>().isKinematic = isKinematic;
    }

    public void IsColider2DEnaled(bool isEnabled)
    {
        childCell.GetComponent<Collider2D>().enabled = isEnabled;
    }

    public void SetLayer(int nummerateLayer)
    {
        childCell.layer = nummerateLayer;
    }

    public void DestoyCell()
    {
        Destroy(childCell);
    }

    private void ChangeLayerDependingOnVelocity()
    {
        bool isCellKinematic = childCell.GetComponent<Rigidbody2D>().isKinematic;
        if (isCellKinematic)
            return;
        Vector2 cellVelocity = childCell.GetComponent<Rigidbody2D>().velocity;
        if (cellVelocity.y < -0.5f)
            childCell.layer = LayerMask.NameToLayer("Neon");
        else if (cellVelocity.y >= -0.5f)
            childCell.layer = LayerMask.NameToLayer("Detection");
    }

    private void OnPause(bool isPause)
    {
        if (isPause)
            PauseObject.Pause(transform);
        else
            PauseObject.UnPause(transform);
    }

    private void OnLoseGame()
    {
        OnPause(true);
    }

    private void Start()
    {
        childCell = gameObject;
    }

    private void OnEnable()
    {
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
        BusEvent.OnPauseEvent += OnPause;
        BusEvent.OnLoseGameEvent += OnLoseGame;
    }

    private void OnDisable()
    {
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        BusEvent.OnPauseEvent -= OnPause;
        BusEvent.OnLoseGameEvent -= OnLoseGame;
    }

    private void FixedUpdate()
    {
        ChangeLayerDependingOnVelocity();
    }
}
