using UnityEngine;


public abstract class FigureBaseState
{
    public abstract void EnterState(FigureFirstMode tetromino);
    public abstract void UpdateState(FigureFirstMode tetromino);
    public abstract void OnTriggerEnter2DState(FigureFirstMode tetromino, Collider2D collision);
    public abstract void OnTriggerExit2DState(FigureFirstMode tetromino, Collider2D collision);
    public abstract void OnCollisionStay2DState(FigureFirstMode tetromino, Collision2D collision);
    public abstract void OnCollisionEnter2DState(FigureFirstMode tetromino, Collision2D collision);
    public abstract void OnDisableState(FigureFirstMode tetromino);
    public abstract void ExitState(FigureFirstMode tetromino);
}

