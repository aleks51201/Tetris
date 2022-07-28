using UnityEngine;

internal class FigureSecondModeQueueState : FigureSecondModeBaseState
{
    private GameObject figure;
    private FigureSecondMode tetr;
    public override void EnterState(FigureSecondMode tetromino)
    {
        StartTetrominoSettigs(tetromino);
        figure = tetromino.tetromino;
        tetr = tetromino;
        BusEvent.OnSpawnTetrominoEvent += ToBoard;
    }

    public override void ExitState(FigureSecondMode tetromino)
    {
        BusEvent.OnSpawnTetrominoEvent -= ToBoard;
    }


    public override void OnCollisionStay2DState(FigureSecondMode tetromino, Collision2D collision)
    {

    }

    public override void OnDisableState(FigureSecondMode tetromino)
    {
        BusEvent.OnSpawnTetrominoEvent -= ToBoard;
    }

    public override void UpdateState(FigureSecondMode tetromino)
    {

    }
    private void StartTetrominoSettigs(FigureSecondMode tetromino)
    {
        tetromino.tetromino.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        tetromino.tetromino.GetComponent<Collider2D>().enabled = false;
        tetromino.ColorationCell();
    }

    private void ToBoard(GameObject fig)
    {
        if (fig == this.figure)
        {
            FigureSecondModeBaseState state = this.tetr.GetState<FigureSecondModeBoardState>();
            tetr.SetState(state);
        }
    }
}

