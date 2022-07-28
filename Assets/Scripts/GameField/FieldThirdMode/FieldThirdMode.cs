using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldThirdMode : FieldBase
{
    [Space]
    [SerializeField]
    [Min(0)]
    private float fallingDelay;
    [SerializeField]
    private float accelerateFallinDelay;

    private List<List<Transform>> matrixField = new();
    private int spawnSpace = 2;
    private float delay;
    private List<Transform> detectedObjects;

    private void CreateMatixField()
    {
        for (int i = 0; i < this.fieldHeight + spawnSpace; i++)
        {
            matrixField.Add(new List<Transform>());
            for (int j = 0; j < this.fieldWidth; j++)
            {
                matrixField[i].Add(null);
            }
        }
    }
    private void AddMatrixTetromino(Transform[] figures)
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
    private void RemoveMatrixTetromino(List<Transform> oldFigurePosition)
    {
        int x;
        int y;
        for (int i = 0; i < oldFigurePosition.Count; i++)
        {
            x = (int)oldFigurePosition[i].position.x;
            y = (int)oldFigurePosition[i].position.y;
            //Destroy(matrixField[y][x].gameObject);
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
    private Transform[] GetChildObject => this.currentTetrominoInGame.GetComponent<FigureThirdMode>().GetAllChildObject();
    private void FallTetromino()
    {
        Vector2 fallDisplacement = new Vector2(0, -1);
        Vector2 oldPosition = this.currentTetrominoInGame.transform.position;
        Vector2 newPosition = oldPosition + fallDisplacement;
        this.currentTetrominoInGame.transform.position = newPosition;
    }
    public bool CanTetrominoMove(Vector2[] currentFigurePositions, Vector2 direction)
    {
        int x;
        int y;
        Vector2 newPosition;
        foreach (Vector2 currentCellPosition in currentFigurePositions)
        {
            newPosition = currentCellPosition + direction;
            x = (int)newPosition.x;
            y = (int)newPosition.y;
            if (0 > x || x >= this.fieldWidth || 0 > y || y >= this.fieldHeight + spawnSpace)
                return false;
            if (matrixField[y][x] != null)
                return false;
        }
        return true;
    }
    private List<Transform> LineDetector()
    {
        List<Transform> detectedObject = new();
        for (int i = 0; i < this.matrixField.Count; i++)
        {
            if (this.matrixField.Count == 0)
                break;
            if (!IsLineFull(matrixField[i]))
                continue;
            foreach (Transform j in matrixField[i])
            {
                detectedObject.Add(j);
            }
        }
        return detectedObject;
    }

    private int NumLine(List<Transform> detectedObject)
    {
        return detectedObject.Count / this.fieldWidth;
    }
    private List<List<Transform>> CreateMatrix(int x, int y)
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

        /*        for(int i=0; i < y; i++)
                {
                    newList[i] = new List<Transform>();
                    for(int j = 0; j < x; j++)
                    {
                        newList[i][j] = null;
                    }
                }
        */
        return newList;
    }

    private void MatrixShift(int shift)
    {
        List<List<Transform>> newList = CreateMatrix(this.fieldWidth, this.fieldHeight);
        /*        Transform cell;
                for(int i=0; i < matrixField.Count; i++)
                {
                    if (i < shift)
                        continue;
                    this.matrixField[i - shift] = this.matrixField[i];
                    for(int j=0; j < this.matrixField[i].Count; j++)
                    {
                        cell = this.matrixField[i][j];
                        if (cell == null)
                            continue;
                        cell.position = new Vector2(j, i - shift);
                    }
                }
        */
        int l = 0;
        for (int i = 0; i < this.matrixField.Count; i++)
        {
            int n = 0;
            foreach (Transform array in this.matrixField[i])
            {
                if (array != null)
                {
                    n++;
                    Vector2 oldPosition = array.position;
                    Vector2 newPosition = new Vector2(oldPosition.x, l);
                    array.position = newPosition;
                }
            }
            if (n > 0)
            {
                newList[l] = matrixField[i];
                l++;
            }
        }

        matrixField = newList;
    }
    private void AddScore()
    {
        gameScore.AddPoint(NumLine(LineDetector()) * 100);
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
    private bool IsFullDetectedList(List<Transform> detectedObjects)
    {
        return detectedObjects.Count >= this.fieldWidth && detectedObjects.Count % this.fieldWidth == 0;
    }

    private IEnumerator AfterDestroyAnimation()
    {
        yield return new WaitForSeconds(0.9f);
        MatrixShift(NumLine(detectedObjects));
        AddScore();
    }
    private void PrintMatrixField()
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
    private protected void EndContolTetromino()
    {
        AddMatrixTetromino(GetChildObject);
        detectedObjects = LineDetector();
        PrintMatrixField();
        if (IsFullDetectedList(detectedObjects))
        {
            StartDestroyAnimation(detectedObjects);
            RemoveMatrixTetromino(detectedObjects);
            StartCoroutine(AfterDestroyAnimation());
        }
        BusEvent.OnCollisionEnterEvent?.Invoke();
    }
    private IEnumerator Falling()
    {
        yield return new WaitForSeconds(delay);
        if (CanTetrominoMove(GetTetrominoCoordinates, new Vector2(0, -1)))
        {
            FallTetromino();
            StartCoroutine(Falling());
        }
        else
            EndContolTetromino();
    }
    private Vector2 BottomCoordinates(Vector2 firstCoordinate, Vector2 secondCoordinate)
    {
        float x1 = firstCoordinate.x;
        float y1 = firstCoordinate.y;
        float x2 = secondCoordinate.x;
        float y2 = secondCoordinate.y;
        if (y1 <= y2)
            return firstCoordinate;
        else
            return secondCoordinate;
    }
    private void MoveParrentObject()
    {
        Vector2[] coordinates = GetTetrominoCoordinates;
        Vector2 newCoordinate = coordinates[0];
        foreach (Vector2 coordinate in coordinates[1..])
        {
            newCoordinate = BottomCoordinates(newCoordinate, coordinate);
        }
        this.currentTetrominoInGame.transform.position = newCoordinate;
    }

    private protected override void SpawnTetromino()
    {
        Create();
        this.currentTetrominoInGame = gameQueue.queueOfTetromino.Dequeue();
        BusEvent.OnSpawnTetrominoEvent?.Invoke(this.currentTetrominoInGame);
        this.currentTetrominoInGame.GetComponent<FigureThirdMode>().field = this;
        this.currentTetrominoInGame.transform.position = spawnPosition;
        StartCoroutine(Falling());
    }

    private void Accelerate(KeyCode keyCode, float _)
    {
        if (keyCode != KeyCode.S)
            return;
        this.delay = this.accelerateFallinDelay;
    }
    private void NormalAccelerate(KeyCode keyCode, float _)
    {
        if (keyCode != KeyCode.S)
            return;
        this.delay = this.fallingDelay;
    }
    private void Start()
    {
        CreateMatixField();
        gameStash = new(stashPosition, spawnPosition);
        gameQueue = new(queueSize, queueShift);
        gameScore = new();
        delay = fallingDelay;
    }
    private void OnEnable()
    {
        BusEvent.OnAddObjectToQueueEvent += Create;
        BusEvent.OnQueueFullEvent += OnQueueFull;
        BusEvent.OnAddScoreEvent += OnAddScore;
        BusEvent.OnLoseGameEvent += OnLoseGame;
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
        BusEvent.OnPauseEvent += IsPaused;
        BusEvent.OnLineIsFullEvent += StartDestroyAnimation;
        //BusEvent.OnLineIsFullEvent += Scoring;
        BusEvent.OnKeyDownEvent += SwitchTetromino;
        BusEvent.OnKeyDownEvent += Accelerate;
        BusEvent.OnKeyUpEvent += NormalAccelerate;
    }
    private void OnDisable()
    {
        BusEvent.OnAddObjectToQueueEvent -= Create;
        BusEvent.OnQueueFullEvent -= OnQueueFull;
        BusEvent.OnAddScoreEvent -= OnAddScore;
        BusEvent.OnLoseGameEvent -= OnLoseGame;
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        BusEvent.OnPauseEvent -= IsPaused;
        BusEvent.OnLineIsFullEvent -= StartDestroyAnimation;
        //BusEvent.OnLineIsFullEvent -= Scoring;
        BusEvent.OnKeyDownEvent -= SwitchTetromino;
        BusEvent.OnKeyDownEvent -= Accelerate;
        BusEvent.OnKeyUpEvent -= NormalAccelerate;
    }
}
