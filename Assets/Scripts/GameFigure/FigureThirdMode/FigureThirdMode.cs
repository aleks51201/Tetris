using System;
using System.Collections.Generic;
using UnityEngine;

public class FigureThirdMode : FigureBase
{
    [Space]
    [HideInInspector]
    public FieldThirdMode field;

    private Dictionary<Type, FigureThirdModeBaseState> statesMap;
    private FigureThirdModeBaseState currentState;

    private void InitState()
    {
        this.statesMap = new Dictionary<Type, FigureThirdModeBaseState>
        {
            [typeof(FigureThirdModeQueueState)] = new FigureThirdModeQueueState(),
            [typeof(FigureThirdModeBoardState)] = new FigureThirdModeBoardState(),
            [typeof(FigureThirdModeStashState)] = new FigureThirdModeStashState()
        };
    }

    public void SetState(FigureThirdModeBaseState newBehaviour)
    {
        if (this.currentState != null)
            this.currentState.ExitState(this);
        this.currentState = newBehaviour;
        this.currentState.EnterState(this);
    }

    public FigureThirdModeBaseState GetState<T>() where T : FigureThirdModeBaseState
    {
        Type type = typeof(T);
        return this.statesMap[type];
    }

    private void SetStateByDefault()
    {
        FigureThirdModeBaseState stateByDefault = this.GetState<FigureThirdModeQueueState>();
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
        if (keyCode != KeyCode.A || !field.CanTetrominoMove(GetTetrominoCoordinates, new Vector2(direct, 0)))
            return;
        Vector2 positionOffset = new Vector2(direct, 0);
        Vector2 newPosition = GetCurrentPosition() + positionOffset;
        this.tetromino.transform.position = newPosition;
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
        if (field.CanTetrominoMove(childLocalCoordinate, new Vector2(0, 0)))
        {
            for (int i = 0; i < allChildObject.Length; i++)
            {
                allChildObject[i].localPosition = childLocalCoordinate[i] - currenParentPosition;
            }
        }
    }

    public override void Acceleration(KeyCode keyCode, float direct)
    {
        /*        if (keyCode != KeyCode.S)
                    return;
                this.tetromino.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,acceleratePower * -1), accelerateForceMode);
        */
    }

    public override Vector2 GetCurrentPosition()
    {
        return this.tetromino.transform.position;
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
        BusEvent.OnDeleteTetrominoEvent?.Invoke(this.tetromino);
        this.tetromino.transform.DetachChildren();
        Destroy(this.tetromino);
    }

    public void Remove()
    {
        Dissolve();
    }

    private void Awake()
    {
        InitState();
        SetStateByDefault();
    }

    private void Start()
    {
    }
    private void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        currentState.OnDisableState(this);
    }
}
