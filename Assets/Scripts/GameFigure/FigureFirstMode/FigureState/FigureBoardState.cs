using System.Collections.Generic;
using UnityEngine;

internal class FigureBoardState : FigureBaseState
{
    private FigureFirstMode tetr;
    private GameObject figure;
    public override void EnterState(FigureFirstMode tetromino)
    {
        tetr = tetromino;
        figure = tetromino.tetromino;

        StartTetrominoSettigs(tetromino);
        BusEvent.OnSwitchTetrominoEvent += OnSwitchTeromino;
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
        BusEvent.OnPauseEvent += OnPause;
        BusEvent.OnKeyDownEvent += tetr.Move;
        BusEvent.OnKeyDownEvent += tetr.Rotate;
        BusEvent.OnKeyDownEvent += tetr.Acceleration;
    }

    public override void ExitState(FigureFirstMode tetromino)
    {
        BusEvent.OnSwitchTetrominoEvent -= OnSwitchTeromino;
        BusEvent.OnPauseEvent -= OnPause;
        BusEvent.OnKeyDownEvent -= tetr.Move;
        BusEvent.OnKeyDownEvent -= tetr.Rotate;
        BusEvent.OnKeyDownEvent -= tetr.Acceleration;
    }

    public override void OnCollisionEnter2DState(FigureFirstMode tetromino, Collision2D collision)
    {
    }

    public override void OnCollisionStay2DState(FigureFirstMode tetromino, Collision2D collision)
    {
        tetromino.DeletingAStopped();
    }

    public override void OnTriggerEnter2DState(FigureFirstMode tetromino, Collider2D collision)
    {
        tetromino.SetNeighborsCoordinates(collision);

    }

    public override void OnTriggerExit2DState(FigureFirstMode tetromino, Collider2D collision)
    {
        tetromino.RemoveNeighborsCoordinates(collision);
    }

    public override void OnDisableState(FigureFirstMode tetromino)
    {
        BusEvent.OnSwitchTetrominoEvent -= OnSwitchTeromino;
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        BusEvent.OnPauseEvent -= OnPause;
        BusEvent.OnKeyDownEvent -= tetr.Move;
        BusEvent.OnKeyDownEvent -= tetr.Rotate;
        BusEvent.OnKeyDownEvent -= tetr.Acceleration;
    }

    public override void UpdateState(FigureFirstMode tetromino)
    {
    }

    private void StartTetrominoSettigs(FigureFirstMode tetromino)
    {
        tetromino.tetromino.GetComponent<Rigidbody2D>().isKinematic = false;
        tetromino.tetromino.GetComponent<Collider2D>().enabled = true;
    }

    private void OnSwitchTeromino()
    {
        FigureBaseState state = this.tetr.GetState<FigureStashState>();
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

    private void OnPause(bool isPause)
    {
        List<Transform> objects = tetr.GetAllChildCell();
        objects.Add(figure.transform);
        foreach (Transform obj in objects)
        {
            if (isPause) 
                PauseObject.Pause(obj);
            else
                PauseObject.UnPause(obj);
        }
    }
}

