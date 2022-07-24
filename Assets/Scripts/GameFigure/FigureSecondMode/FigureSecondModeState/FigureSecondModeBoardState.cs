using UnityEngine;

class FigureSecondModeBoardState : FigureSecondModeBaseState
{
    FigureSecondMode tetr;
    private GameObject figure;
    public override void EnterState(FigureSecondMode tetromino)
    {
        tetr = tetromino;
        figure = tetromino.tetromino;
        StartTetrominoSettigs(tetromino);
        BusEvent.OnSwitchTerominoEvent += OnSwitchTeromino;
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
        BusEvent.OnKeyHoldEvent += tetr.Move;
        BusEvent.OnKeyHoldEvent += tetr.Rotate;
        BusEvent.OnKeyHoldEvent += tetr.Acceleration;
    }

    public override void ExitState(FigureSecondMode tetromino)
    {
        BusEvent.OnSwitchTerominoEvent -= OnSwitchTeromino;
        BusEvent.OnKeyHoldEvent -= tetr.Move;
        BusEvent.OnKeyHoldEvent -= tetr.Rotate;
        BusEvent.OnKeyHoldEvent -= tetr.Acceleration;
    }

    public override void OnCollisionStay2DState(FigureSecondMode tetromino, Collision2D collision)
    {
        tetromino.DeletingAStopped();
    }

    public override void OnDisableState(FigureSecondMode tetromino)
    {
        BusEvent.OnSwitchTerominoEvent -= OnSwitchTeromino;
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        BusEvent.OnKeyHoldEvent -= tetr.Move;
        BusEvent.OnKeyHoldEvent -= tetr.Rotate;
        BusEvent.OnKeyHoldEvent -= tetr.Acceleration;
    }

    public override void UpdateState(FigureSecondMode tetromino)
    {
    }
    private void StartTetrominoSettigs(FigureSecondMode tetromino)
    {
        tetromino.tetromino.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        tetromino.tetromino.GetComponent<Collider2D>().enabled = true;
    }
    private void OnSwitchTeromino()
    {
       FigureSecondModeBaseState state = this.tetr.GetState<FigureSecondModeStashState>();
        tetr.SetState(state);

    }
    private void OnDeleteTetromino(GameObject figure)
    {
        if (figure == this.figure)
        {
            BusEvent.OnSwitchTerominoEvent -= OnSwitchTeromino;
            BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        }
    }

}

