using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldThirdMode : FieldBase
{
    private protected List<List<Transform>> matrixField = new();
    private int spawnSpace = 2;
    [HideInInspector]
    public List<Transform> detectedObjects;

    public List<List<Transform>> MatrixField => matrixField;
    public int SpawnSpace => spawnSpace;

    private protected List<List<Transform>> CreateMatrix(int x, int y)
    {
        List<List<Transform>> newList = new();
        for (int i = 0; i < y + spawnSpace; i++)
        {
            newList.Add(new List<Transform>());
            for (int j = 0; j < x; j++)
            {
                newList[i].Add(null);
            }
        }
        return newList;
    }

    private void CreateMatixField()
    {
        this.matrixField = CreateMatrix(this.fieldWidth, this.fieldHeight);
    }

    public void AddMatrixTetromino(Transform[] figures)
    {
        int x;
        int y;
        for (int i = 0; i < figures.Length; i++)
        {
            x = (int)figures[i].position.x;
            y = (int)figures[i].position.y;
            if (y > this.fieldHeight)
                OnLoseGame();
            matrixField[y][x] = figures[i];
        }
    }

    public void RemoveMatrixTetromino(List<Transform> oldFigurePosition)
    {
        int x;
        int y;
        for (int i = 0; i < oldFigurePosition.Count; i++)
        {
            x = (int)oldFigurePosition[i].position.x;
            y = (int)oldFigurePosition[i].position.y;
            matrixField[y][x] = null;
        }
    }

    private Vector2[] GetTetrominoCoordinates
    {
        get
        {
            Vector2[] newArray = new Vector2[4];
            List<Vector2> oldList = this.currentTetrominoInGame.GetComponent<FigureThirdMode>().GetChildCoordinate();
            for (int i = 0; i < oldList.Count; i++)
            {
                newArray[i] = oldList[i];
            }
            return newArray;
        }
    }

    private protected Transform[] GetChildObject => this.currentTetrominoInGame.GetComponent<FigureThirdMode>().GetAllChildObject();

    public virtual List<Transform> LineDetector()
    {
        List<Transform> detectedObject = new();
        for (int i = 0; i < this.matrixField.Count; i++)
        {
            if (!IsLineFull(this.matrixField[i]))
                continue;
            foreach (Transform cell in matrixField[i])
            {
                detectedObject.Add(cell);
            }
        }
        return detectedObject;
    }

    private int NumLine(List<Transform> detectedObject)
    {
        return detectedObject.Count / this.fieldWidth;
    }

    private protected virtual void MatrixShift()
    {
        List<List<Transform>> newList = CreateMatrix(this.fieldWidth, this.fieldHeight);
        int j = 0;
        for (int i = 0; i < this.matrixField.Count; i++)
        {
            int n = 0;
            foreach (Transform cell in this.matrixField[i])
            {
                if (cell != null)
                {
                    n++;
                    Vector2 oldPosition = cell.position;
                    Vector2 newPosition = new Vector2(oldPosition.x, j);
                    cell.position = newPosition;
                }
            }
            if (n > 0)
            {
                newList[j] = this.matrixField[i];
                j++;
            }
        }
        this.matrixField = newList;
    }

    private void AddScore()
    {
        gameScore.AddPoint(NumLine(detectedObjects) * 100);
    }
    private bool IsLineFull(List<Transform> lineForCheck)
    {
        for (int i = 0; i < lineForCheck.Count; i++)
        {
            if (lineForCheck[i] == null)
                return false;
        }
        return true;
    }

    public virtual bool IsFullDetectedList(List<Transform> detectedObjects)
    {
        return detectedObjects.Count >= this.fieldWidth && detectedObjects.Count % this.fieldWidth == 0;
    }

    public void StartAfterDestroyAnimation()
    {
        StartCoroutine(AfterDestroyAnimation());
    }
    private IEnumerator AfterDestroyAnimation()
    {
        yield return new WaitForSeconds(0.9f);
        MatrixShift();
        AddScore();
    }

    public void PrintMatrixField()//?????
    {
        string line = "";
        for (int i = this.matrixField.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < this.matrixField[i].Count; j++)
            {
                if (this.matrixField[i][j] == null)
                    line += 0;
                else
                    line += 1 + " ";
            }
            line += "\n";
        }
        Debug.Log(line);
    }

    private protected override void SpawnTetromino()
    {
        Create();
        this.currentTetrominoInGame = gameQueue.queueOfTetromino.Dequeue();
        BusEvent.OnSpawnTetrominoEvent?.Invoke(this.currentTetrominoInGame);
        this.currentTetrominoInGame.GetComponent<FigureThirdMode>().field = this;
        this.currentTetrominoInGame.transform.position = spawnPosition;
        StartCoroutine(this.currentTetrominoInGame.GetComponent<FigureThirdMode>().Falling());
    }

    private void Start()
    {
        CreateMatixField();
        gameStash = new(stashPosition, spawnPosition);
        gameQueue = new(queueSize, queueShift);
        gameScore = new();
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
