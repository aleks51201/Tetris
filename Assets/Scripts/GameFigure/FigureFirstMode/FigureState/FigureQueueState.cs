using UnityEngine;


class FigureQueueState : FigureBaseState
{
    private GameObject figure;
    private FigureFirstMode tetr;
    public override void EnterState(FigureFirstMode tetromino)
    {
        StartTetrominoSettigs(tetromino);
        figure = tetromino.tetromino;
        tetr = tetromino;
        BusEvent.OnSpawnTetrominoEvent += ToBoard;
    }

    public override void ExitState(FigureFirstMode tetromino)
    {
        BusEvent.OnSpawnTetrominoEvent -= ToBoard;
    }

    public override void FixedUpdateState(FigureFirstMode tetromino)
    {

    }

    public override void OnCollisionEnter2DState(FigureFirstMode tetromino, Collision2D collision)
    {

    }

    public override void OnCollisionStay2DState(FigureFirstMode tetromino, Collision2D collision)
    {

    }

    public override void OnDisableState(FigureFirstMode tetromino)
    {
      //  BusEvent.OnSpawnTetrominoEvent -= ToBoard;
    }

    public override void OnEnableState(FigureFirstMode tetromino)
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
        tetromino.ColorationCell();
    }

    private void ToBoard(GameObject fig)
    {
        if (fig == this.figure)
        {
            FigureBaseState state = this.tetr.GetState<FigureBoardState>();
            tetr.SetState(state);
        }
    }
}

