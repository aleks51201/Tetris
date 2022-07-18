using UnityEngine;


public abstract class FigureBaseState
{
    public abstract void EnterState(Figure tetromino);
    public abstract void UpdateState(Figure tetromino);
    public abstract void FixedUpdateState(Figure tetromino);
    public abstract void OnTriggerEnter2DState(Figure tetromino, Collider2D collision);
    public abstract void OnTriggerExit2DState(Figure tetromino, Collider2D collision);
    public abstract void OnCollisionStay2DState(Figure tetromino, Collision2D collision);
    public abstract void OnCollisionEnter2DState(Figure tetromino, Collision2D collision);
    public abstract void OnEnableState(Figure tetromino);
    public abstract void OnDisableState(Figure tetromino);
    public abstract void ExitState(Figure tetromino);
}

