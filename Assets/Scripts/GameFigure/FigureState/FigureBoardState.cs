﻿using UnityEngine;

class FigureBoardState : FigureBaseState
{
    Figure tetr;
    private GameObject figure;
    public override void EnterState(Figure tetromino)
    {
        StartTetrominoSettigs(tetromino);
        BusEvent.OnSwitchTerominoEvent += OnSwitchTeromino;
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
        tetr = tetromino;
        figure = tetromino.tetromino;
    }

    public override void ExitState(Figure tetromino)
    {
        BusEvent.OnSwitchTerominoEvent -= OnSwitchTeromino;
    }

    public override void FixedUpdateState(Figure tetromino)
    {

    }

    public override void OnCollisionEnter2DState(Figure tetromino, Collision2D collision)
    {

    }

    public override void OnCollisionStay2DState(Figure tetromino, Collision2D collision)
    {
        tetromino.DeletingAStopped();
    }

    public override void OnTriggerEnter2DState(Figure tetromino, Collider2D collision)
    {
        tetromino.SetNeighborsCoordinates(collision);

    }

    public override void OnTriggerExit2DState(Figure tetromino, Collider2D collision)
    {
        tetromino.RemoveNeighborsCoordinates(collision);
    }

    public override void OnEnableState(Figure tetromino)
    {
    }

    public override void OnDisableState(Figure tetromino)
    {
        BusEvent.OnSwitchTerominoEvent -= OnSwitchTeromino;
        //BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
    }

    public override void UpdateState(Figure tetromino)
    {
        if (tetromino.inputX != 0)
        {
            tetromino.HandleMove();
            tetromino.inputX = 0;
        }
        if (tetromino.rotate)
        {
            tetromino.HandleRotate();
            tetromino.rotate = false;
        }
        tetromino.HandleAcceleration();
    }
    private void StartTetrominoSettigs(Figure tetromino)
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
            BusEvent.OnSwitchTerominoEvent -= OnSwitchTeromino;
            BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        }
    }

}
