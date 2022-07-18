using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Figure : MonoBehaviour, IControlable
{
    [System.NonSerialized] public float inputX;
    [System.NonSerialized] public sbyte accelerate;
    [System.NonSerialized] public bool rotate;
    [SerializeField] public GameObject tetromino;
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

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
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
    private void OnEnable()
    {
        currentState.OnEnableState(this); 
    }
    private void OnDisable()
    {
        currentState.OnDisableState(this); 
    }
    public void Move(float direct)
    {
        this.inputX = direct;
    }

    public void HandleMove()
    {
        if (IsNeighborsEmpty(this.inputX))
        {
            Vector2 positionOffset = new Vector2(inputX, 0);
            Vector2 newPosition = GetCurrentPosition() + positionOffset;

            this.tetromino.transform.position = newPosition;
        }   
    }

    public void Acceleration(sbyte toggle)
    {
        accelerate = toggle;
    }

    public void HandleAcceleration()
    {
        if (accelerate==1)
        {
            // this.tetromino.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -6);
            this.tetromino.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -0.001f), ForceMode2D.Impulse);
            BusEvent.OnStartAccelerationEvent?.Invoke(GetCurrentPosition());
            accelerate = 0;
        }            
        else if(accelerate == -1)
        {
            //this.tetromino.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -3);
            BusEvent.OnEndAccelerationEvent?.Invoke(GetCurrentPosition());
            accelerate = 0;
        }
    }

    public void Rotate(bool toggle)
    {
        this.rotate = toggle;
    }

    public void HandleRotate()
    {
        if (!IsRotateAllowed())
            this.tetromino.transform.Rotate(new Vector3(0f, 0f, 1), 90, Space.Self);
    }

    private bool IsRotateAllowed()
    {
        Collider2D ghost = GetChildGhost().GetComponent<Collider2D>();
        return ghost.IsTouchingLayers(layerMask);
    }

    public void ColorationCell()
    {
        Color hue = RandomColorFigureGame();
        foreach (Transform cell in GetAllChildCell())
        {
            cell.GetComponent<SpriteRenderer>().color = hue;
        }
    }

    private Color RandomColorFigureGame()
    {
        System.Random random = new();
        Color[] colorArray = ColorDataHolder.colorInGameFigure;
        return colorArray[random.Next(0, colorArray.Length)];
    }

    private void Dissolve()
    {
        if (isDelete)
        {
            DestroyGhost();
            BusEvent.OnDeleteTetrominoEvent?.Invoke(this.tetromino);
            this.tetromino.transform.DetachChildren();
            Destroy(this.tetromino);
            isDelete = false;
        }       
    }

    private void DestroyGhost()
    {
        Destroy(GetChildGhost().gameObject);
    }

    public void DeletingAStopped()
    {
        if (this.tetromino.GetComponent<Rigidbody2D>().velocity.y > -0.5f && this.tetromino != null)
            Dissolve();
    }

    public Vector2 GetCurrentPosition()
    {
        return this.tetromino.transform.position;
    }

    public Transform[] GetAllChildObject()
    {
        return this.tetromino.GetComponentsInChildren<Transform>()[1..^0];
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

    public List<Vector3> GetCoodinate()
    {
        List<Vector3> coordinates = new();
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

        foreach (Vector3 childCoordinate in GetCoodinate())
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
}

