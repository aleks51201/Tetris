using System.Collections;
using UnityEngine;


public class Conrol : MonoBehaviour
{
<<<<<<< Updated upstream
    private IMove figure;   
=======
    private IControlable figure;
    private ICreatable tetrominoCreate;
>>>>>>> Stashed changes

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            float Call = Input.GetAxisRaw("Horizontal");
            if (Call != 0)
            {
                figure.Move(Call);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            figure.Rotate();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
<<<<<<< Updated upstream
            figure.Create();
=======
            tetrominoCreate.Create();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Kill();
>>>>>>> Stashed changes
        }
    }
}
