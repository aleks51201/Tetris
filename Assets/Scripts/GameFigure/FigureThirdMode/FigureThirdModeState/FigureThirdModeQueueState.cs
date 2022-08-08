using UnityEngine;

internal class FigureThirdModeQueueState : FigureThirdModeBaseState
{
    private GameObject figure;
    private FigureThirdMode tetr;

    public override void EnterState(FigureThirdMode tetromino)
    {
        figure = tetromino.tetromino;
        tetr = tetromino;
        tetromino.ColorationCell();
        BusEvent.OnSpawnTetrominoEvent += ToBoard;
    }

    public override void ExitState(FigureThirdMode tetromino)
    {
        BusEvent.OnSpawnTetrominoEvent -= ToBoard;
    }

    public override void OnDisableState(FigureThirdMode tetromino)
    {
        BusEvent.OnSpawnTetrominoEvent -= ToBoard;
    }

    public override void UpdateState(FigureThirdMode tetromino)
    {
    }

    private void ToBoard(GameObject fig)
    {
        if (fig == this.figure)
        {
            FigureThirdModeBaseState state = this.tetr.GetState<FigureThirdModeBoardState>();
            tetr.SetState(state);
        }
    }
}
