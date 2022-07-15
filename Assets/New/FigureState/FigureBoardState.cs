using UnityEngine;

class FigureBoardState : FigureBaseState
{
    public override void EnterState(Figure tetromino)
    {
        StartTetrominoSettigs(tetromino);
    }

    public override void ExitState(Figure tetromino)
    {
    }

    public override void FixedUpdateState(Figure tetromino)
    {

    }

    public override void OnCollisionEnter2DState(Figure tetromino, Collision2D collision)
    {
        tetromino.DeletingAStopped();
    }

    public override void OnCollisionStay2DState(Figure tetromino, Collision2D collision)
    {
        
    }

    public override void OnTriggerEnter2DState(Figure tetromino, Collider2D collision)
    {
        tetromino.SetNeighborsCoordinates(collision);
        
    }

    public override void OnTriggerExit2DState(Figure tetromino, Collider2D collision)
    {
        tetromino.RemoveNeighborsCoordinates(collision);
    }

    public override void UpdateState(Figure tetromino)
    {
        if (tetromino.inputX != 0)
        {
            tetromino.HandleMove();
            tetromino.inputX = 0;
        }
        if (tetromino.rotate)
        {
            tetromino.HandleRotate();
            tetromino.rotate = false;
        }    
        tetromino.HandleAcceleration();
    }
    private void StartTetrominoSettigs(Figure tetromino)
    {
        tetromino.tetromino.GetComponent<Rigidbody2D>().isKinematic = false;
        tetromino.tetromino.GetComponent<Collider2D>().enabled = true;
    }
}

