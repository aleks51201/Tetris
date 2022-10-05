using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureThirdMode : FigureBase
{
    [Space]
    [HideInInspector]
    public FieldThirdMode field;
    [Space]
    [SerializeField]
    [Min(0)]
    private float fallingDelay;
    [SerializeField]
    public float accelerateFallinDelay;


    private float delay;
    private Dictionary<Type, FigureThirdModeBaseState> statesMap;
    private FigureThirdModeBaseState currentState;
    [HideInInspector]
    public Coroutine falling;

    private void InitState()
    {
        statesMap = new Dictionary<Type, FigureThirdModeBaseState>
        {
            [typeof(FigureThirdModeQueueState)] = new FigureThirdModeQueueState(),
            [typeof(FigureThirdModeBoardState)] = new FigureThirdModeBoardState(),
            [typeof(FigureThirdModeStashState)] = new FigureThirdModeStashState()
        };
    }

    public void SetState(FigureThirdModeBaseState newBehaviour)
    {
        if (currentState != null)
            currentState.ExitState(this);
        currentState = newBehaviour;
        currentState.EnterState(this);
    }

    public FigureThirdModeBaseState GetState<T>() where T : FigureThirdModeBaseState
    {
        Type type = typeof(T);
        return statesMap[type];
    }

    private void SetStateByDefault()
    {
        FigureThirdModeBaseState stateByDefault = GetState<FigureThirdModeQueueState>();
        SetState(stateByDefault);
    }

    private Vector2[] GetTetrominoCoordinates
    {
        get
        {
            Vector2[] newArray = new Vector2[4];
            List<Vector2> oldList = GetChildCoordinate();
            for (int i = 0; i < oldList.Count; i++)
            {
                newArray[i] = oldList[i];
            }
            return newArray;
        }
    }

    public override void Move(KeyCode keyCode, float direct)
    {
        if (keyCode != KeyCode.A || !CanTetrominoMove(GetTetrominoCoordinates, new Vector2(direct, 0)))
            return;
        Vector2 positionOffset = new Vector2(direct, 0);
        Vector2 newPosition = GetCurrentPosition() + positionOffset;
        tetromino.transform.position = newPosition;
    }

    public override void Rotate(KeyCode keyCode, float direct)
    {
        if (keyCode != KeyCode.R)
            return;
        Transform[] allChildObject = GetAllChildObject();
        Vector2[] childLocalCoordinate = new Vector2[4];
        Vector2 currenParentPosition = GetCurrentPosition();
        Vector2 newPosition;
        for (int i = 0; i < allChildObject.Length; i++)
        {
            newPosition = new(-allChildObject[i].localPosition.y, allChildObject[i].localPosition.x);
            childLocalCoordinate[i] = newPosition + currenParentPosition;
        }
        if (CanTetrominoMove(childLocalCoordinate, new Vector2(0, 0)))
        {
            for (int i = 0; i < allChildObject.Length; i++)
            {
                allChildObject[i].localPosition = childLocalCoordinate[i] - currenParentPosition;
            }
        }
    }

    public override void Acceleration(KeyCode keyCode, float direct)
    {
    }

    public override Vector2 GetCurrentPosition()
    {
        return tetromino.transform.position;
    }

    public override void ColorationCell()
    {
        Color hue = RandomColorFigureGame();
        foreach (Transform cell in GetAllChildObject())
        {
            cell.GetComponent<SpriteRenderer>().color = hue;
        }
    }

    private protected override Color RandomColorFigureGame()
    {
        System.Random random = new();
        Color[] colorArray = ColorDataHolder.colorInGameFigure;
        return colorArray[random.Next(0, colorArray.Length)];
    }

    public override List<Vector2> GetChildCoordinate()
    {
        List<Vector2> coordinates = new();
        foreach (Transform child in GetAllChildObject())
        {
            coordinates.Add(child.position);
        }
        return coordinates;
    }

    private protected override void Dissolve()
    {
        ParticleStart();
        BusEvent.OnDeleteTetrominoEvent?.Invoke(tetromino);
        tetromino.transform.DetachChildren();
        Destroy(tetromino);
    }

    public void Remove()
    {
        Dissolve();
    }

    private void FallTetromino()
    {
        Vector2 fallDisplacement = new Vector2(0, -1);
        Vector2 oldPosition = tetromino.transform.position;
        Vector2 newPosition = oldPosition + fallDisplacement;
        tetromino.transform.position = newPosition;
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
            if (0 > x || x >= field.FieldWidth || 0 > y || y >= field.FieldHeigh + field.SpawnSpace)
                return false;
            if (field.MatrixField[y][x] != null)
                return false;
        }
        return true;
    }

    private protected virtual void EndContolTetromino()
    {
        field.AddMatrixTetromino(GetAllChildObject());
        field.detectedObjects = field.LineDetector();
        if (field.IsFullDetectedList(field.detectedObjects))
        {
            field.StartDestroyAnimation(field.detectedObjects);
            field.RemoveMatrixTetromino(field.detectedObjects);
            field.StartAfterDestroyAnimation(field.detectedObjects, field.GameScore);
        }
        BusEvent.OnCollisionEnterEvent?.Invoke();
    }

    public IEnumerator Falling()
    {
        yield return new WaitForSeconds(delay);
        if (CanTetrominoMove(GetTetrominoCoordinates, new Vector2(0, -1)))
        {
            FallTetromino();
            falling = StartCoroutine(Falling());
            yield break;
        }
        EndContolTetromino();
    }

    public void Accelerate(KeyCode keyCode, float _)
    {
        if (keyCode != KeyCode.S)
            return;
        delay = accelerateFallinDelay;
    }

    public void NormalAccelerate(KeyCode keyCode, float _)
    {
        if (keyCode != KeyCode.S)
            return;
        delay = fallingDelay;
    }

    public void IsPaused(bool isPaused)
    {
        if (isPaused)
        {
            StopAllCoroutines();
            return;
        }
        StartCoroutine(Falling());
    }


    private void Awake()
    {
        InitState();
        SetStateByDefault();
    }

    private void Start()
    {
        delay = fallingDelay;
    }
    private void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnDisable()
    {
        currentState.OnDisableState(this);
    }
}
