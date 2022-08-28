using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class FieldFourthMode : FieldThirdMode
{
    private ScoreTreeInRow gameScore; 
   private List<Vector2> savedCoordinate;
    private int cellCombo = 3;
    public new ScoreTreeInRow GameScore => gameScore;


    public override void MatrixShift()
    {
        List<Transform> detectedObjects;
        List<List<Transform>> newMatrix = CreateMatrix(fieldWidth, FieldHeigh);
        for (int x = 0; x < fieldWidth; x++)
        {
            int rowIndex = 0;
            for (int y = 0; y < FieldHeigh; y++)
            {
                Transform cell = matrixField[y][x];
                if (cell == null)
                    continue;
                cell.position = new Vector2(x, rowIndex);
                newMatrix[rowIndex][x] = cell;
                rowIndex++;
            }
        }
        matrixField = newMatrix;
        detectedObjects = LineDetector(matrixField);
        if (IsFullDetectedList(detectedObjects))
        {
            StartDestroyAnimation(detectedObjects);
            RemoveMatrixTetromino(detectedObjects);
            StartAfterDestroyAnimation(detectedObjects,GameScore);
        }
    }

    private void SaveCoordinate(List<Transform> detectedObjects)
    {
        savedCoordinate = new();
        if (detectedObjects.Count < 2)
            return;
        foreach (Transform cell in detectedObjects)
        {
            savedCoordinate.Add(cell.position);
        }
    }

    private List<Transform> FindChainOnHorizontal(Transform cell, Color hue)
    {
        List<Transform> detectedObjects = new();
        Vector2[] directions = { new Vector2(1, 0), new Vector2(-1, 0) };
        foreach (Vector2 direct in directions)
        {
            detectedObjects.AddRange(ChainWalk(cell.position, direct, hue));
        }
        detectedObjects.Add(cell);
        if (detectedObjects.Count < 3)
            return new List<Transform>();
        return detectedObjects;
    }

    private List<Transform> FindChainOnVertical(Transform cell, Color hue)
    {
        List<Transform> detectedObjects = new();
        Vector2[] directions = { new Vector2(0, 1), new Vector2(0, -1), };
        foreach (Vector2 direct in directions)
        {
            detectedObjects.AddRange(ChainWalk(cell.position, direct, hue));
        }
        detectedObjects.Add(cell);
        if (detectedObjects.Count < 3)
            return new List<Transform>();
        return detectedObjects;
    }

    private List<Transform> FindChainOnRightDiagonal(Transform cell, Color hue)
    {
        List<Transform> detectedObjects = new();
        Vector2[] directions = { new Vector2(1, 1), new Vector2(-1, -1), };
        foreach (Vector2 direct in directions)
        {
            detectedObjects.AddRange(ChainWalk(cell.position, direct, hue));
        }
        detectedObjects.Add(cell);
        if (detectedObjects.Count < 3)
            return new List<Transform>();
        return detectedObjects;
    }

    private List<Transform> FindChainOnLeftDiagonal(Transform cell, Color hue)
    {
        List<Transform> detectedObjects = new();
        Vector2[] directions = { new Vector2(-1, 1), new Vector2(1, -1), };
        foreach (Vector2 direct in directions)
        {
            detectedObjects.AddRange(ChainWalk(cell.position, direct, hue));
        }
        detectedObjects.Add(cell);
        if (detectedObjects.Count < 3)
            return new List<Transform>();
        return detectedObjects;
    }

    private List<Transform> FindChainOnDirections(Transform cell, Color hue)
    {
        List<Transform> detectedObjects = new();
        List<Transform>[] chains =
        {
            FindChainOnVertical(cell, hue),
            FindChainOnHorizontal(cell, hue),
            FindChainOnRightDiagonal(cell, hue),
            FindChainOnLeftDiagonal(cell, hue),
        };
        foreach (List<Transform> chain in chains)
        {
            detectedObjects.AddRange(chain);
        }
        List<Transform> distinctDetectedObjects = detectedObjects.Distinct().ToList();
        return distinctDetectedObjects;
    }

    public override List<Transform> LineDetector()
    {
        Transform[] childObjects = GetChildObject;
        Color hue;
        List<Transform> detectedObjects = new();
        for (int i = 0; i < childObjects.Length; i++)
        {
            Transform cell = childObjects[i];
            hue = cell.GetComponent<SpriteRenderer>().color;
            detectedObjects.AddRange(FindChainOnDirections(cell, hue));
        }
        /*        List<Transform> distinctDetectedObjects = detectedObjects.Distinct().ToList();
                SaveCoordinate(distinctDetectedObjects);
        */
        this.detectedObjects = detectedObjects.Distinct().ToList();
        SaveCoordinate(this.detectedObjects);
        return this.detectedObjects;
    }

    public List<Transform> LineDetector(List<List<Transform>> field)
    {
        Color hue;
        List<Transform> detectedObjects = new();
        foreach (List<Transform> row in field)
        {
            foreach (Transform cell in row)
            {
                if (cell == null)
                    continue;
                hue = cell.GetComponent<SpriteRenderer>().color;
                detectedObjects.AddRange(FindChainOnDirections(cell, hue));
            }
        }
        //List<Transform> distinctDetectedObjects = detectedObjects.Distinct().ToList();
        this.detectedObjects = detectedObjects.Distinct().ToList();
        //SaveCoordinate(distinctDetectedObjects);
        SaveCoordinate(this.detectedObjects);
        return this.detectedObjects;
    }

    private bool IsOutOfRange(int x, int y)
    {
        return !(0 <= x && x < FieldWidth && 0 <= y && y < FieldHeigh);
    }

    private bool IsCell(int x, int y)
    {
        return matrixField[y][x] != null;
    }

    private List<Transform> ChainWalk(Vector2 startPosition, Vector2 direction, Color color)
    {
        List<Transform> detectedObjects = new();
        Color currentColor;
        int x = (int)startPosition.x;
        int y = (int)startPosition.y;
        while (true)
        {
            x += (int)direction.x;
            y += (int)direction.y;
            if (IsOutOfRange(x, y) || !IsCell(x, y))
                return detectedObjects;
            Transform cell = matrixField[y][x];
            currentColor = cell.GetComponent<SpriteRenderer>().color;
            if (currentColor != color)
                return detectedObjects;
            if (!detectedObjects.Exists(isInside => isInside == cell))
                detectedObjects.Add(cell);
        }
    }

    public override bool IsFullDetectedList(List<Transform> detectedObjects)
    {
        return detectedObjects.Count >= 3;
    }
    private void AddScore(List<Transform> detectedObjects,ScoreTreeInRow scoreObject )
    {
        scoreObject.CalcPoint(detectedObjects);
    }


    public void StartAfterDestroyAnimation(List<Transform> detectedObjects,ScoreTreeInRow scoreObject)
    {
        StartCoroutine(AfterDestroyAnimation(detectedObjects,scoreObject));
    }

    private IEnumerator AfterDestroyAnimation(List<Transform> detectedObjects,ScoreTreeInRow scoreObject)
    {
        yield return new WaitForSeconds(0.9f);
        AddScore(detectedObjects,scoreObject);
        MatrixShift();
    }

    private protected override void OnLoseGame()
    {
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        losePanel.SetActive(true);
        loseScorePanel.text = $"{gameScore.Point}";
    }

    private void Start()
    {
        CreateMatixField();
        gameStash = new(stashPosition, spawnPosition);
        gameQueue = new(queueSize, queueShift);
        gameScore = new(cellCombo);
    }

    private void OnEnable()
    {
        BusEvent.OnAddObjectToQueueEvent += Create;
        BusEvent.OnQueueFullEvent += OnQueueFull;
        BusEvent.OnAddScoreEvent += OnAddScore;
        BusEvent.OnLoseGameEvent += OnLoseGame;
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
        BusEvent.OnLineIsFullEvent += StartDestroyAnimation;
        BusEvent.OnKeyDownEvent += SwitchTetromino;
    }

    private void OnDisable()
    {
        BusEvent.OnAddObjectToQueueEvent -= Create;
        BusEvent.OnQueueFullEvent -= OnQueueFull;
        BusEvent.OnAddScoreEvent -= OnAddScore;
        BusEvent.OnLoseGameEvent -= OnLoseGame;
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        BusEvent.OnLineIsFullEvent -= StartDestroyAnimation;
        BusEvent.OnKeyDownEvent -= SwitchTetromino;
    }
}
