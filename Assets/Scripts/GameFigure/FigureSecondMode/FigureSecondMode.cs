using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FigureSecondMode : FigureBase
{
    [Header("Move")]
    [SerializeField]
    private ForceMode2D moveForceMode;
    [SerializeField]
    [Range(0,1)]
    private float movePower;

    [Header("Rotate")]
    [SerializeField]
    private ForceMode2D rotateForceMode;
    [SerializeField]
    private float torque;

    [Header("Acceleration")]
    [SerializeField]
    private ForceMode2D accelerateForceMode;
    [SerializeField]
    [Range(0,1)]
    private float acceleratePower;

    private Dictionary<Type, FigureSecondModeBaseState> statesMap;
    private FigureSecondModeBaseState currentState;

    private bool isDelete = true;

    private void InitState()
    {
        this.statesMap = new Dictionary<Type, FigureSecondModeBaseState>();
        this.statesMap[typeof(FigureSecondModeQueueState)] = new FigureSecondModeQueueState();
        this.statesMap[typeof(FigureSecondModeBoardState)] = new FigureSecondModeBoardState();
        this.statesMap[typeof(FigureSecondModeStashState)] = new FigureSecondModeStashState();
    }

    public  void SetState(FigureSecondModeBaseState newBehaviour)
    {
        if (this.currentState != null)
            this.currentState.ExitState(this);
        this.currentState = newBehaviour;
        this.currentState.EnterState(this);
    }

    public FigureSecondModeBaseState GetState<T>() where T : FigureSecondModeBaseState
    {
        var type = typeof(T);
        return this.statesMap[type];
    }

    private void SetStateByDefault()
    {
        var stateByDefault = this.GetState<FigureSecondModeQueueState>();
        SetState(stateByDefault);
    }

    public override void Move(KeyCode keyCode,float direct)
    {
        if (keyCode != KeyCode.A)
            return;
        this.tetromino.GetComponent<Rigidbody2D>().AddForce(new Vector2(movePower*direct,0), moveForceMode);
    }

    public override void Rotate(KeyCode keyCode,float direct)
    {
        if (keyCode != KeyCode.R)
            return;
        Rigidbody2D body = this.tetromino.GetComponent<Rigidbody2D>();
        float impulse = (torque* Mathf.Deg2Rad) * body.inertia;
        body.AddTorque(impulse, rotateForceMode);
    }

    public override void Acceleration(KeyCode keyCode,float direct)
    {
        if (keyCode != KeyCode.S)
            return;
        this.tetromino.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,acceleratePower * -1), accelerateForceMode);
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
    private protected override void Dissolve()
    {
        if (isDelete)
        {
            isDelete = false;
            ParticleStart();
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

    public override Vector2 GetCurrentPosition()
    {
        return this.tetromino.transform.position;
    }

    public override List<Vector3> GetChildCoordinate()
    {
        List<Vector3> coordinates = new();
        foreach (Transform child in GetAllChildObject())
        {
            coordinates.Add(child.position);
        }
        return coordinates;
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        currentState.OnCollisionStay2DState(this, collision);
    }
    private void OnDisable()
    {
        currentState.OnDisableState(this);
    }
}
