public abstract class FigureThirdModeBaseState
{
    public abstract void EnterState(FigureThirdMode tetromino);
    public abstract void UpdateState(FigureThirdMode tetromino);
    public abstract void OnDisableState(FigureThirdMode tetromino);
    public abstract void ExitState(FigureThirdMode tetromino);

}
