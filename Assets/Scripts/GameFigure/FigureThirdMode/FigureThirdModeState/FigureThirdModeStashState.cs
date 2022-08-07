internal class FigureThirdModeStashState : FigureThirdModeBaseState
{
    private FigureThirdMode tetr;

    public override void EnterState(FigureThirdMode tetromino)
    {
        tetr = tetromino;
        BusEvent.OnSwitchTetrominoEvent += OnSwitchTetromino;
        tetr.StopCoroutine(tetr.falling);
    }

    public override void ExitState(FigureThirdMode tetromino)
    {
        BusEvent.OnSwitchTetrominoEvent -= OnSwitchTetromino;
    }

    public override void OnDisableState(FigureThirdMode tetromino)
    {
        BusEvent.OnSwitchTetrominoEvent -= OnSwitchTetromino;
    }

    public override void UpdateState(FigureThirdMode tetromino)
    {
    }

    private void OnSwitchTetromino()
    {
        FigureThirdModeBaseState state = this.tetr.GetState<FigureThirdModeBoardState>();
        tetr.SetState(state);
    }
}
