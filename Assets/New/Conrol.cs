using System.Collections;
using UnityEngine;


public class Conrol : MonoBehaviour
{
    public GameObject gameField;
    private GameObject inGameTetromino;
    
    private Field field;



    private void Start()
    {
        field = gameField.GetComponent<Field>();
        field.OnCreateTetrominoEvent += OnCreateTetromino;
    }
    private void FixedUpdate()
    {
        
    }


    private void OnCreateTetromino(GameObject newTetromino)
    {
        Debug.Log("create tetr") ;
        this.inGameTetromino = newTetromino;
    }
    void Update()
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

        if (Input.GetKeyDown(KeyCode.S))
        {
            /*figure.Rotate();*/
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ICreatable tetrominoCreate = gameField.GetComponent<ICreatable>();
            if (tetrominoCreate != null)
            {
                tetrominoCreate.Create();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Kill();
        }
    }
}
