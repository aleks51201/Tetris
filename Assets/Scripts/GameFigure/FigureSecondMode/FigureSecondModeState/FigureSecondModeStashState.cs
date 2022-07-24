using UnityEngine;

class FigureSecondModeStashState : FigureSecondModeBaseState
{
    private FigureSecondMode tetr;
    public override void EnterState(FigureSecondMode tetromino)
    {
        StartTetrominoSettigs(tetromino);
        BusEvent.OnSwitchTerominoEvent += OnSwitchTetromino;
        tetr = tetromino;
    }

    public override void ExitState(FigureSecondMode tetromino)
    {
        BusEvent.OnSwitchTerominoEvent -= OnSwitchTetromino;
    }


    public override void OnCollisionStay2DState(FigureSecondMode tetromino, Collision2D collision)
    {

    }

    public override void UpdateState(FigureSecondMode tetromino)
    {

    }

    private void StartTetrominoSettigs(FigureSecondMode tetromino)
    {
        tetromino.tetromino.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        tetromino.tetromino.GetComponent<Collider2D>().enabled = false;
    }
    private void OnSwitchTetromino()
    {
        FigureSecondModeBaseState state = this.tetr.GetState<FigureSecondModeBoardState>();
        tetr.SetState(state);
    }

    public override void OnDisableState(FigureSecondMode tetromino)
    {
        BusEvent.OnSwitchTerominoEvent -= OnSwitchTetromino;
    }
}

