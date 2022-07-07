using System.Collections;
using UnityEngine;


public class Conrol : MonoBehaviour
{
    private IMove figure;   

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
            figure.Create();
        }
    }
}
