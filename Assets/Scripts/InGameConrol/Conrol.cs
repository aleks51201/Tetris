using UnityEngine;


public class Conrol : MonoBehaviour
{
    public GameObject gameField;
    private bool isPaused = true;

    private void OnEnable()
    {
        BusEvent.OnPauseEvent += OnPause;
    }

    private void OnDisable()
    {
        BusEvent.OnPauseEvent -= OnPause;
        BusEvent.OnLoseGameEvent -= OnLose;
    }

    private void OnPause(bool isPaused)
    {
        this.isPaused = isPaused;
    }
    private void OnLose()
    {
        OnPause(true);
    }

    private void FixedUpdate()
    {
        if (isPaused)
            return;

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

    private void Update()
    {
        if (isPaused)
            return;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BusEvent.OnKeyDownEvent?.Invoke(KeyCode.Escape, 0);
        }
    }
}
