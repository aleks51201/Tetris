using UnityEngine;


public class Conrol : MonoBehaviour
{
    public GameObject gameField;
    private GameObject inGameTetromino;


    private void OnEnable()
    {
        BusEvent.OnSpawnTetrominoEvent += OnCreateTetromino;
    }
    private void OnDisable()
    {
        BusEvent.OnSpawnTetrominoEvent -= OnCreateTetromino;
    }

    private void OnCreateTetromino(GameObject newTetromino)
    {
        this.inGameTetromino = newTetromino;
    }
    void Update()
    {
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
        }



    }
}
