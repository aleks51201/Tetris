using UnityEngine;


public abstract class FigureSecondModeBaseState 
{
    public abstract void EnterState(FigureSecondMode tetromino);
    public abstract void UpdateState(FigureSecondMode tetromino);
    public abstract void OnCollisionStay2DState(FigureSecondMode tetromino, Collision2D collision);
    public abstract void OnDisableState(FigureSecondMode tetromino);
    public abstract void ExitState(FigureSecondMode tetromino);
}

