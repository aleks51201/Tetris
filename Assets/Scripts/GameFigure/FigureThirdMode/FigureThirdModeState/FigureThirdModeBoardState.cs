using UnityEngine;

internal class FigureThirdModeBoardState : FigureThirdModeBaseState
{
    private FigureThirdMode tetr;
    private GameObject figure;

    public override void EnterState(FigureThirdMode tetromino)
    {
        tetr = tetromino;
        figure = tetromino.tetromino;
        tetr.falling = tetr.StartCoroutine(tetr.Falling());
        BusEvent.OnSwitchTetrominoEvent += OnSwitchTeromino;
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
        BusEvent.OnCollisionEnterEvent += OnCollision;
        BusEvent.OnKeyDownEvent += tetr.Move;
        BusEvent.OnKeyDownEvent += tetr.Rotate;
        BusEvent.OnKeyDownEvent += tetr.Accelerate;
        BusEvent.OnKeyUpEvent += tetr.NormalAccelerate;
        BusEvent.OnPauseEvent += tetr.IsPaused;
    }

    public override void ExitState(FigureThirdMode tetromino)
    {
        BusEvent.OnSwitchTetrominoEvent -= OnSwitchTeromino;
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        BusEvent.OnCollisionEnterEvent -= OnCollision;
        BusEvent.OnKeyDownEvent -= tetr.Move;
        BusEvent.OnKeyDownEvent -= tetr.Rotate;
        BusEvent.OnKeyDownEvent -= tetr.Acceleration;
        BusEvent.OnKeyDownEvent -= tetr.Accelerate;
        BusEvent.OnKeyUpEvent -= tetr.NormalAccelerate;
        BusEvent.OnPauseEvent -= tetr.IsPaused;
    }
    public override void OnDisableState(FigureThirdMode tetromino)
    {
        ExitState(tetromino);
    }

    public override void UpdateState(FigureThirdMode tetromino)
    {
    }

    private void OnSwitchTeromino()
    {
        FigureThirdModeBaseState state = this.tetr.GetState<FigureThirdModeStashState>();
        tetr.SetState(state);
    }

    private void OnDeleteTetromino(GameObject figure)
    {
        if (figure == this.figure)
        {
            BusEvent.OnSwitchTetrominoEvent -= OnSwitchTeromino;
            BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        }
    }

    private void OnCollision()
    {
        tetr.Remove();
    }
}
