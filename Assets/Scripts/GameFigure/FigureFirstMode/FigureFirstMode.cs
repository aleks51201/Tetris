using System;
using System.Collections.Generic;
using UnityEngine;


public class FigureFirstMode : FigureBase
{
    [SerializeField] private LayerMask layerMask;

    private List<GameObject> rightNeighbours = new();
    private List<GameObject> leftNeighbours = new();

    private Dictionary<Type, FigureBaseState> statesMap;
    private FigureBaseState currentState;



    private bool isDelete = true;

    private void InitState()
    {
        this.statesMap = new Dictionary<Type, FigureBaseState>();
        this.statesMap[typeof(FigureQueueState)] = new FigureQueueState();
        this.statesMap[typeof(FigureBoardState)] = new FigureBoardState();
        this.statesMap[typeof(FigureStashState)] = new FigureStashState();
    }

    public void SetState(FigureBaseState newBehaviour)
    {
        if (this.currentState != null)
            this.currentState.ExitState(this);
        this.currentState = newBehaviour;
        this.currentState.EnterState(this);
    }

    public FigureBaseState GetState<T>() where T : FigureBaseState
    {
        var type = typeof(T);
        return this.statesMap[type];
    }

    private void SetStateByDefault()
    {
        var stateByDefault = this.GetState<FigureQueueState>();
        SetState(stateByDefault);
    }

    public override void Move(KeyCode keyCode, float direct)
    {
        if (keyCode != KeyCode.A)
            return;
        if (IsNeighborsEmpty(direct))
        {
            Vector2 positionOffset = new Vector2(direct, 0);
            Vector2 newPosition = GetCurrentPosition() + positionOffset;

            this.tetromino.transform.position = newPosition;
        }
    }


    public override void Acceleration(KeyCode keyCode, float direct)
    {
        if (keyCode != KeyCode.S)
            return;
        this.tetromino.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -0.001f), ForceMode2D.Impulse);
    }

    public override void Rotate(KeyCode keyCode, float direct)
    {
        if (keyCode != KeyCode.R)
            return;
        if (!IsRotateAllowed())
            this.tetromino.transform.Rotate(new Vector3(0f, 0f, 1), 90, Space.Self);
    }


    private bool IsRotateAllowed()
    {
        Collider2D ghost = GetChildGhost().GetComponent<Collider2D>();
        return ghost.IsTouchingLayers(layerMask);
    }


    public override void ColorationCell()
    {
        Color hue = RandomColorFigureGame();
        foreach (Transform cell in GetAllChildCell())
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

    private protected override void Dissolve()
    {
        if (isDelete)
        {
            isDelete = false;
            ParticleStart();
            DestroyGhost();
            BusEvent.OnDeleteTetrominoEvent?.Invoke(this.tetromino);
            this.tetromino.transform.DetachChildren();
            Destroy(this.tetromino);
        }

    }
    public void DeletingAStopped()
    {
        if (this.tetromino.GetComponent<Rigidbody2D>().velocity.y > -0.5f && this.tetromino != null)
            Dissolve();
    }

    private void DestroyGhost()
    {
        Destroy(GetChildGhost().gameObject);
    }

    public override Vector2 GetCurrentPosition()
    {
        return this.tetromino.transform.position;

    }


    public Transform GetChildGhost()
    {
        Transform[] childObjects = GetAllChildObject();
        for (int i = 0; i < childObjects.Length; i++)
        {
            if (childObjects[i].CompareTag("Ghost"))
            {
                return childObjects[i];
            }
        }
        throw new NullReferenceException($"Ghost not found {childObjects}");
    }

    public List<Transform> GetAllChildCell()
    {
        Transform[] childObject = GetAllChildObject();
        Transform childCell;
        List<Transform> childCells = new();
        for (int i = 0; i < childObject.Length; i++)
        {
            childCell = childObject[i];
            if (childCell.CompareTag("Figure"))
            {
                childCells.Add(childCell);
            }
        }
        return childCells;
    }

    public override List<Vector2> GetChildCoordinate()
    {
        List<Vector2> coordinates = new();
        foreach (Transform child in GetAllChildCell())//?????
        {
            coordinates.Add(child.position);
        }
        return coordinates;
    }

    public void SetNeighborsCoordinates(Collider2D collider)
    {
        Vector3 unallocatedCoordinate = collider.transform.position;
        Vector3 rightNeighbor = unallocatedCoordinate;
        Vector3 leftNeighbor = unallocatedCoordinate;

        foreach (Vector3 childCoordinate in GetChildCoordinate())
        {
            if (childCoordinate.x <= unallocatedCoordinate.x)
            {
                if (rightNeighbours.Exists(i => i == collider.gameObject))
                    continue;
                rightNeighbours.Add(collider.gameObject);
            }
            else if (childCoordinate.x >= unallocatedCoordinate.x && leftNeighbor != childCoordinate)
            {
                if (leftNeighbours.Exists(i => i == collider.gameObject))
                    continue;
                leftNeighbours.Add(collider.gameObject);
            }
        }
    }

    public void RemoveNeighborsCoordinates(Collider2D collider)
    {
        rightNeighbours.RemoveAll(i => i == collider.gameObject);
        leftNeighbours.RemoveAll(i => i == collider.gameObject);
    }

    private bool IsNeighborsEmpty(float direction)
    {
        switch (direction)
        {
            case 1:
                return rightNeighbours.Count == 0;
            case -1:
                return leftNeighbours.Count == 0;
            default:
                throw new NullReferenceException("IsNeighborsEmpty: don't have any direction");
        }
    }
    private void Awake()
    {
        InitState();
        SetStateByDefault();
    }
    private void Update()
    {
        currentState.UpdateState(this);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnTriggerEnter2DState(this, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentState.OnTriggerExit2DState(this, collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        currentState.OnCollisionStay2DState(this, collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2DState(this, collision);
    }
    private void OnDisable()
    {
        currentState.OnDisableState(this);
    }

}

