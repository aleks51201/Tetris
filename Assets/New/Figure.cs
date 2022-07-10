using System.Collections;
using System.Collections.Generic;
using UnityEngine;


<<<<<<< Updated upstream
public class Figure : MonoBehaviour, IMove
{
    GameObject figureL;
    public void Create()
    {
       
       Instantiate(this.figureL, new Vector3(5f, 20f, 0f), Quaternion.identity);
       
    }
=======
public class Figure : MonoBehaviour, IControlable, IGetFigureCellCoordinate
{
    public GameObject tetromino;
    public Vector2 figureSpawnPosition;

>>>>>>> Stashed changes

    public void Remove()
    {
        
    }

    public void Move(float direct)
    {
        Vector2 positionOffset = new Vector2(direct, 0);
<<<<<<< Updated upstream
        Vector2 startPosition = figureL.transform.position;
        Quaternion originalRotation = figureL.transform.rotation;
        Vector2 newPosition = startPosition + positionOffset;

        this.figureL.transform.position = newPosition;
=======
        Vector2 newPosition = GetCurrentPosition() + positionOffset;

        this.tetromino.transform.position = newPosition;
>>>>>>> Stashed changes

    }
    
    public void Rotate()
    {
<<<<<<< Updated upstream
        this.figureL.transform.Rotate(new Vector3(0f, 0f, 1), 90, Space.Self);
=======
        this.tetromino.transform.Rotate(new Vector3(0f, 0f, 1), 90, Space.Self);
>>>>>>> Stashed changes
    }

    public void Dissolve()
    {
<<<<<<< Updated upstream

=======
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
>>>>>>> Stashed changes
    }
}