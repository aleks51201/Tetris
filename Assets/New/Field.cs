using System.Collections.Generic;
using UnityEngine;


public class Field : MonoBehaviour, ICreatable
{
    [Header("Teromino list")]
    public GameObject[] tetrominoCollection;

    [Header("Figure start position")]
    public Vector2 figureSpawnPosition;

    [Header("Detector start position")]
    public Vector3 detectorStartPosition;

    [Header("Layer lvl")]
    public string maskName;

    [Header("Field size")]
    private int widthField = 10;
    private int heightField = 20;

  

    //public GameObject currentTetromino { get; private set; }

    private IGetFigureCellCoordinate coordinate;
    private GameObject currentTetromino;

    

    private RaycastHit2D[] GetDetectionObject(Vector3 detectorPosition)
    {
        return Physics2D.RaycastAll(detectorPosition, Vector2.right, this.widthField, GetLayerMask(this.maskName));
    }

    private int GetLayerMask(string maskName)
    {
        return LayerMask.GetMask(maskName);
    }

    private void PatroDetector(Vector2 detectorStartPosition)
    {
       // Vector2 offsetPosition=new Vector2();
        Vector2 newPosition;
        for(int i= 0; i< this.heightField; i++)
        {
            newPosition = detectorStartPosition + new Vector2 (0,i);
            if (GetDetectionObject(newPosition).Length == 0)
                break;
            CheckLineFull(GetDetectionObject(newPosition));
        }
    }
    private void CheckLineFull(RaycastHit2D[] detectedObject)
    {
        Debug.Log(detectedObject.Length);
        if (detectedObject.Length >= widthField * 2)//???? '2'
            
            DestroyObject(detectedObject);
    }

    public void DestroyObject(RaycastHit2D[] listOfDetectionObject)
    {
        foreach (RaycastHit2D i in listOfDetectionObject)
        {
            Destroy(i.transform.gameObject);
        }
    }


    private GameObject RandomFigureSelection(GameObject[] tetrominoCollection)
    {
        System.Random randomValue = new();
        int index = randomValue.Next(0, tetrominoCollection.Length);
        return tetrominoCollection[index];
    }

    public delegate void CreateHandler(GameObject newTetromino);
    public event CreateHandler OnCreateTetrominoEvent;
    public void Create()
    {
        currentTetromino = Instantiate(RandomFigureSelection(this.tetrominoCollection), figureSpawnPosition, Quaternion.identity);
        OnCreateTetrominoEvent?.Invoke(currentTetromino);
    }


    private void FixedUpdate()
    {
        PatroDetector(this.detectorStartPosition);
    }



}
