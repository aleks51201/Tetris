using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Figure : MonoBehaviour, IMove
{
    GameObject figureL;
    public void Create()
    {
       
       Instantiate(this.figureL, new Vector3(5f, 20f, 0f), Quaternion.identity);
       
    }

    public void Remove()
    {
        
    }

    public void Move(float direct)
    {
        Vector2 positionOffset = new Vector2(direct, 0);
        Vector2 startPosition = figureL.transform.position;
        Quaternion originalRotation = figureL.transform.rotation;
        Vector2 newPosition = startPosition + positionOffset;

        this.figureL.transform.position = newPosition;

    }
    
    public void Rotate()
    {
        this.figureL.transform.Rotate(new Vector3(0f, 0f, 1), 90, Space.Self);
    }

    public void Dissolve()
    {

    }
}