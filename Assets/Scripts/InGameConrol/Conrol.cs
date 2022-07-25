using UnityEngine;


public class Conrol : MonoBehaviour
{
    public GameObject gameField;
    private GameObject inGameTetromino;
    private bool isPaused = false;

    private void OnEnable()
    {
        BusEvent.OnPauseEvent += OnPause;
        BusEvent.OnSpawnTetrominoEvent += OnCreateTetromino;
    }
    private void OnDisable()
    {
        BusEvent.OnPauseEvent -= OnPause;
        BusEvent.OnSpawnTetrominoEvent -= OnCreateTetromino;
    }

    private void OnCreateTetromino(GameObject newTetromino)
    {
        this.inGameTetromino = newTetromino;
    }
    private void OnPause(bool isPaused)
    {
        this.isPaused = isPaused;
    }
/*    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log("Detected key code: " + e.keyCode);
        }
    }*/
    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            float call = Input.GetAxisRaw("Horizontal");
            if (call != 0)
            {
                BusEvent.OnKeyHoldEvent?.Invoke(KeyCode.A, call);
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            BusEvent.OnKeyHoldEvent?.Invoke(KeyCode.R, 1);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            BusEvent.OnKeyHoldEvent?.Invoke(KeyCode.R, -1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            BusEvent.OnKeyHoldEvent?.Invoke(KeyCode.S, 0);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            float call = Input.GetAxisRaw("Horizontal");
            if (call != 0)
            {
                BusEvent.OnKeyDownEvent?.Invoke(KeyCode.A, call);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            BusEvent.OnKeyDownEvent?.Invoke(KeyCode.R, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            BusEvent.OnKeyDownEvent?.Invoke(KeyCode.S, 0);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            BusEvent.OnKeyUpEvent?.Invoke(KeyCode.S, 0);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            BusEvent.OnKeyDownEvent?.Invoke(KeyCode.F, 0);
        }


        /*        if (isPaused)
                    return;
                if (inGameTetromino != null)
                {
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                    {
                        float Call = Input.GetAxisRaw("Horizontal");
                        if (Call != 0)
                        {
                            IControlable figure = inGameTetromino.GetComponent<IControlable>();
                            if (this.inGameTetromino != null)
                                figure.Move(Call);
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        IControlable figure = inGameTetromino.GetComponent<IControlable>();
                        if (this.inGameTetromino != null)
                            figure.Rotate(true);
                    }
                    if (Input.GetKeyDown(KeyCode.S))
                    {

                        IControlable figure = inGameTetromino.GetComponent<IControlable>();
                        if (this.inGameTetromino != null)
                            figure.Acceleration(1);
                    }
                    if (Input.GetKeyUp(KeyCode.S))
                    {
                        IControlable figure = inGameTetromino.GetComponent<IControlable>();
                        if (this.inGameTetromino != null)
                            figure.Acceleration(-1);
                    }
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ISwitchTetromino tetrominoSwitch = gameField?.GetComponent<ISwitchTetromino>();
                    if (tetrominoSwitch != null)
                    {
                        tetrominoSwitch.SwitchTetromino();
                    }

                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    //Kill();
                }*/


    }
}
