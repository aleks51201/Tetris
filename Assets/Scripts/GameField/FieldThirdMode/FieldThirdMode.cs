using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldThirdMode : FieldBase
{
    private protected List<List<Transform>> matrixField = new();
    private int spawnSpace = 6;
    [HideInInspector]
    public List<Transform> detectedObjects;

    public List<List<Transform>> MatrixField => matrixField;
    private ScoreClassic gameScore;
    public int SpawnSpace => spawnSpace;
    public ScoreClassic GameScore => gameScore;

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

    private protected void CreateMatixField()
    {
        matrixField = CreateMatrix(fieldWidth, fieldHeight);
    }

    public void AddMatrixTetromino(Transform[] figures)
    {
        int x;
        int y;
        for (int i = 0; i < figures.Length; i++)
        {
            x = (int)figures[i].position.x;
            y = (int)figures[i].position.y;
            if (y > fieldHeight)
            {
                BusEvent.OnLoseGameEvent?.Invoke();
                OnLoseGame();
            }
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

    private protected Transform[] GetChildObject => currentTetrominoInGame.GetComponent<FigureThirdMode>().GetAllChildObject();

    public virtual List<Transform> LineDetector()
    {
        List<Transform> detectedObject = new();
        for (int i = 0; i < matrixField.Count; i++)
        {
            if (!IsLineFull(matrixField[i]))
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
        return detectedObject.Count / fieldWidth;
    }

    public virtual void MatrixShift()
    {
        List<List<Transform>> newList = CreateMatrix(fieldWidth, fieldHeight);
        int j = 0;
        for (int i = 0; i < matrixField.Count; i++)
        {
            int n = 0;
            foreach (Transform cell in matrixField[i])
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
                newList[j] = matrixField[i];
                j++;
            }
        }
        matrixField = newList;
    }

    private void AddScore(List<Transform> detectedObjects, ScoreClassic scoreObject)
    {
        scoreObject.CalcPoint(detectedObjects);
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
        return detectedObjects.Count >= fieldWidth && detectedObjects.Count % fieldWidth == 0;
    }

    public void StartAfterDestroyAnimation(List<Transform> detectedObjects, ScoreClassic scoreObject)
    {
        StartCoroutine(AfterDestroyAnimation(detectedObjects, scoreObject));
    }

    private IEnumerator AfterDestroyAnimation(List<Transform> detectedObjects, ScoreClassic scoreObject)
    {
        yield return new WaitForSeconds(0.9f);
        AddScore(detectedObjects, scoreObject);
        MatrixShift();
    }

    private protected virtual void OnLoseGame()
    {
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        losePanel.SetActive(true);
        loseScorePanel.text = $"{gameScore.Point}";
    }

    public void PrintMatrixField()//?????
    {
        string line = "";
        for (int i = matrixField.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < matrixField[i].Count; j++)
            {
                if (matrixField[i][j] == null)
                    line += 0;
                else
                    line += 1 + " ";
            }
            line += "\n";
        }
        Debug.Log(line);
    }
    private void OnCollision()
    {
        AddMatrixTetromino(currentTetrominoInGame.GetComponent<FigureThirdMode>().GetAllChildObject());
        detectedObjects = LineDetector();
        if (IsFullDetectedList(detectedObjects))
        {
            StartDestroyAnimation(detectedObjects);
            RemoveMatrixTetromino(detectedObjects);
        }
    }

    private protected virtual void AfterDestroyAnimation()
    {
        AddScore(detectedObjects, GameScore);
        MatrixShift();
    }


    private protected override void SpawnTetromino()
    {
        Create();
        currentTetrominoInGame = gameQueue.queueOfTetromino.Dequeue();
        BusEvent.OnSpawnTetrominoEvent?.Invoke(currentTetrominoInGame);
        currentTetrominoInGame.GetComponent<FigureThirdMode>().field = this;
        currentTetrominoInGame.transform.position = spawnPosition;
    }

    private void Start()
    {
        CreateMatixField();
        gameStash = new(stashPosition, spawnPosition);
        gameQueue = new(queueSize, queueShift);
        gameScore = new ScoreClassic(fieldWidth);
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
        BusEvent.OnCollisionEnterEvent += OnCollision;
        BusEvent.OnStartAfterDestoyAnimation += AfterDestroyAnimation;
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
        BusEvent.OnCollisionEnterEvent -= OnCollision;
        BusEvent.OnStartAfterDestoyAnimation -= AfterDestroyAnimation;
    }
}

