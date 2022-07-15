using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class FigureQueueState : FigureBaseState
{
    private GameObject figure;
    private Figure tetr;
    public override void EnterState(Figure tetromino)
    {
        StartTetrominoSettigs(tetromino);
        figure = tetromino.tetromino;
        tetr = tetromino;
        BusEvent.OnSpawnTetrominoEvent += ToBoard;
    }

    public override void ExitState(Figure tetromino)
    {
        BusEvent.OnSpawnTetrominoEvent -= ToBoard;
    }

    public override void FixedUpdateState(Figure tetromino)
    {
        
    }

    public override void OnCollisionEnter2DState(Figure tetromino, Collision2D collision)
    {
        
    }

    public override void OnCollisionStay2DState(Figure tetromino, Collision2D collision)
    {
        
    }

    public override void OnTriggerEnter2DState(Figure tetromino, Collider2D collision)
    {
        
    }

    public override void OnTriggerExit2DState(Figure tetromino, Collider2D collision)
    {
        
    }

    public override void UpdateState(Figure tetromino)
    {
        
    }
    private void StartTetrominoSettigs(Figure tetromino)
    {
        tetromino.tetromino.GetComponent<Rigidbody2D>().isKinematic = true;
        tetromino.tetromino.GetComponent<Collider2D>().enabled = false;
        tetromino.ColorationCell();
    }

    private void ToBoard(GameObject fig)
    {
        if (fig == this.figure)
        {
            FigureBaseState state = this.tetr.GetState<FigureBoardState>();
            tetr.SetState(state);
        }        
    }
}

