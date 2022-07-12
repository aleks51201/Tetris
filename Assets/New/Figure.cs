using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Figure : MonoBehaviour, IControlable, IGetFigureCellCoordinate
{
    public GameObject tetromino;


    public void Remove()
    {
        
    }

    public void Move(float direct)
    {
        Vector2 positionOffset = new Vector2(direct, 0);
        Vector2 newPosition = GetCurrentPosition() + positionOffset;

        Debug.Log(positionOffset);
        Debug.Log(newPosition);

        this.tetromino.transform.position = newPosition;

    }
    
    public void Rotate()
    {
        this.tetromino.transform.Rotate(new Vector3(0f, 0f, 1), 90, Space.Self);
        Debug.Log(GetCurrentPosition());
    }

    public void Dissolve()
    {
        this.tetromino.transform.DetachChildren();
    }

    //Ghost ????
    public Vector2 GetCurrentPosition()
    {
        return  this.tetromino.transform.position;
    }

    public GameObject[] GetAllChildObjects()
    {
        return this.tetromino.GetComponentsInChildren<GameObject>()[1..^ 0] ;
    }

    public List<Vector3> GetCoodinate()
    {
        List<Vector3> coordinates = new();
        foreach (GameObject i in GetAllChildObjects())
        {
            coordinates.Add(i.transform.position);
        }
        return coordinates;
    }
}