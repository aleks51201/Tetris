using System;
using UnityEngine;

public class Stash
{
    private GameObject currentTetromino;
    private Vector2 stashPosition;
    private Vector2 spawnPosition;
    public bool isItSwithcable = true;

    public Stash(Vector2 stashPosition, Vector2 spawnPosition)
    {
        this.stashPosition = stashPosition;
        this.spawnPosition = spawnPosition;
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
    }
    private void SetStashItem(GameObject tetromino)
    {
        this.currentTetromino = tetromino;
        this.currentTetromino.transform.position = this.stashPosition;
    }
    private GameObject GetStashItem()
    {
        GameObject outputFigure = this.currentTetromino;
        this.currentTetromino = null;
        outputFigure.transform.position = this.spawnPosition;
        return outputFigure;
    }

    public GameObject SwitchTetromino(GameObject tetromino)
    {
        this.isItSwithcable = false;
        GameObject outputFigure=null;
        if (this.currentTetromino != null)
        {
            outputFigure = GetStashItem();
            BusEvent.OnSpawnTetrominoEvent?.Invoke(outputFigure);
        }
        SetStashItem(tetromino);
        BusEvent.OnSwitchTerominoEvent?.Invoke();
        return outputFigure;
    }
    
    private void OnDeleteTetromino(GameObject _)
    {
        this.isItSwithcable = true;
    }
}

