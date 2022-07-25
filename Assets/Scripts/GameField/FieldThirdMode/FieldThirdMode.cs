using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class FieldThirdMode : FieldBase
{
    [Space]
    [SerializeField]
    private float fallingSpeed;

    private List<List<int>> matrixField = new();
    private int fallingFigure= 2;
    private int frozenFigure = 1;
    private int nonFigure = 0;
    private void CreateMatixField()
    {
        int spawnSpace = 2;
        for (int i = 0; i < this.fieldHeight + spawnSpace; i++)
        {
            matrixField.Add(new List<int>());
            for (int j = 0; j < this.fieldWidth; j++)
            {
                matrixField[i].Add(0);
            }
        }
    }
    private void AddMatrixTetromino(Vector2 figurePosition)
    {
        int x = (int)figurePosition.x;
        int y = (int)figurePosition.y;
        matrixField[y][x] = this.fallingFigure;
    }
    private void RemoveMatrixTetromino(Vector2 oldFigurePosition)
    {
        int x = (int)oldFigurePosition.x;
        int y = (int)oldFigurePosition.y;
        matrixField[y][x] = this.nonFigure;
    }
    private void FreezeMatrixTetromino(Vector2 figurePosition)
    {
        int x = (int)figurePosition.x;
        int y = (int)figurePosition.y;
        matrixField[y][x] = this.frozenFigure;
    }
    private List<Vector2> GetTetrominoCoordinates => this.currentTetrominoInGame.GetComponent<FigureThirdMode>().GetChildCoordinate();
    private void FallTetromino()
    {
        Vector2 fallDisplacement = new Vector2(0, -1);
        for (int i = 0; i < GetTetrominoCoordinates.Count; i++)
        {
            RemoveMatrixTetromino(GetTetrominoCoordinates[i]);
            GetTetrominoCoordinates[i] += fallDisplacement;
            AddMatrixTetromino(GetTetrominoCoordinates[i]);
        }
    }
    private bool CanTetrominoMove(List<Vector2> currentFigurePositions, Vector2 direction)
    {
        int x;
        int y;
        Vector2 newPosition;
        foreach(Vector2 currentCellPosition in currentFigurePositions)
        {
            newPosition = currentCellPosition + direction;
            x = (int)newPosition.x;
            y = (int)newPosition.y;
            if (0 > x || x >= this.fieldWidth || 0>y || y >= this.fieldHeight)
                return false;
            if (matrixField[y][x] != 0)
                return false;
        }
        return true;
    }

    private void EndContolTetromino()
    {

    }
    private IEnumerator Falling()
    {
        yield return new WaitForSeconds(fallingSpeed);
        if (CanTetrominoMove(GetTetrominoCoordinates,new Vector2(0, -1)))
        {
            FallTetromino();
            Falling();
        }
        else
            EndContolTetromino();
    }
    private void Start()
    {
        CreateMatixField();
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
}
