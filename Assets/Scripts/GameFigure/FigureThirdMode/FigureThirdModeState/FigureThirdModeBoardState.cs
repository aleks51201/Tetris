using UnityEngine;

internal class FigureThirdModeBoardState : FigureThirdModeBaseState
{
    private FigureThirdMode tetr;
    private GameObject figure;
    public override void EnterState(FigureThirdMode tetromino)
    {
        tetr = tetromino;
        figure = tetromino.tetromino;
        BusEvent.OnSwitchTerominoEvent += OnSwitchTeromino;
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
        BusEvent.OnCollisionEnterEvent += OnCollision;
        BusEvent.OnKeyDownEvent += tetr.Move;
        BusEvent.OnKeyDownEvent += tetr.Rotate;
        BusEvent.OnKeyDownEvent += tetr.Acceleration;
    }

    public override void ExitState(FigureThirdMode tetromino)
    {
        BusEvent.OnSwitchTerominoEvent -= OnSwitchTeromino;
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        BusEvent.OnCollisionEnterEvent -= OnCollision;
        BusEvent.OnKeyDownEvent -= tetr.Move;
        BusEvent.OnKeyDownEvent -= tetr.Rotate;
        BusEvent.OnKeyDownEvent -= tetr.Acceleration;
    }

    public override void OnDisableState(FigureThirdMode tetromino)
    {
        BusEvent.OnSwitchTerominoEvent -= OnSwitchTeromino;
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        BusEvent.OnCollisionEnterEvent -= OnCollision;
        BusEvent.OnKeyDownEvent -= tetr.Move;
        BusEvent.OnKeyDownEvent -= tetr.Rotate;
        BusEvent.OnKeyDownEvent -= tetr.Acceleration;
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
            BusEvent.OnSwitchTerominoEvent -= OnSwitchTeromino;
            BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        }
    }
    private void OnCollision()
    {
        tetr.Remove();
    }
}
