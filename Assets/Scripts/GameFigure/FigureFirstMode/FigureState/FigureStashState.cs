using UnityEngine;

class FigureStashState : FigureBaseState
{
    private FigureFirstMode tetr;
    public override void EnterState(FigureFirstMode tetromino)
    {
        StartTetrominoSettigs(tetromino);
        BusEvent.OnSwitchTerominoEvent += OnSwitchTetromino;
        tetr = tetromino;
    }

    public override void ExitState(FigureFirstMode tetromino)
    {
        BusEvent.OnSwitchTerominoEvent -= OnSwitchTetromino;
    }

    public override void OnCollisionEnter2DState(FigureFirstMode tetromino, Collision2D collision)
    {

    }

    public override void OnCollisionStay2DState(FigureFirstMode tetromino, Collision2D collision)
    {

    }

    public override void OnTriggerEnter2DState(FigureFirstMode tetromino, Collider2D collision)
    {

    }

    public override void OnTriggerExit2DState(FigureFirstMode tetromino, Collider2D collision)
    {

    }

    public override void UpdateState(FigureFirstMode tetromino)
    {

    }

    private void StartTetrominoSettigs(FigureFirstMode tetromino)
    {
        tetromino.tetromino.GetComponent<Rigidbody2D>().isKinematic = true;
        tetromino.tetromino.GetComponent<Collider2D>().enabled = false;
        tetromino.tetromino.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
    private void OnSwitchTetromino()
    {
        FigureBaseState state = this.tetr.GetState<FigureBoardState>();
        tetr.SetState(state);
    }

    public override void OnDisableState(FigureFirstMode tetromino)
    {
        BusEvent.OnSwitchTerominoEvent -= OnSwitchTetromino;
    }
}

