using UnityEngine;

class FigureStashState : FigureBaseState
{
    private Figure tetr;
    public override void EnterState(Figure tetromino)
    {
        StartTetrominoSettigs(tetromino);
        BusEvent.OnSwitchTerominoEvent += OnSwitchTetromino;
        tetr = tetromino;
    }

    public override void ExitState(Figure tetromino)
    {
        BusEvent.OnSwitchTerominoEvent -= OnSwitchTetromino;
    }

    public override void FixedUpdateState(Figure tetromino)
    {

    }

    public override void OnCollisionEnter2DState(Figure tetromino, Collision2D collision)
    {

    }

    public override void OnCollisionStay2DState(Figure tetromino, Collision2D collision)
    {

    }

    public override void OnTriggerEnter2DState(Figure tetromino, Collider2D collision)
    {

    }

    public override void OnTriggerExit2DState(Figure tetromino, Collider2D collision)
    {

    }

    public override void UpdateState(Figure tetromino)
    {

    }

    private void StartTetrominoSettigs(Figure tetromino)
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

    public override void OnEnableState(Figure tetromino)
    {
    }

    public override void OnDisableState(Figure tetromino)
    {
        BusEvent.OnSwitchTerominoEvent -= OnSwitchTetromino;
    }
}

