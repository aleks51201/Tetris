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
    private List<Vector3> rightNeighbours = new();
    private List<Vector3> leftNeighbours = new();
    

    private RaycastHit2D[] GetDetectionObject(Vector3 detectorPosition)
    {
        return Physics2D.RaycastAll(detectorPosition, Vector2.right, this.widthField, GetLayerMask(this.maskName));
    }

    private int GetLayerMask(string maskName)
    {
        return LayerMask.GetMask(name);
    }

    private void PatroDetector(Vector2 detectorStartPosition)
    {
        Vector2 offsetPosition = new(0, 1);
        Vector2 newPosition;
        for(int i= 0; i< this.widthField; i++)
        {
            newPosition = detectorStartPosition + offsetPosition;
            CheckLineFull(GetDetectionObject(newPosition));
        }
    }
    private void CheckLineFull(RaycastHit2D[] detectedObject)
    {
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

    private void SetNeighborsCoordinates(Collider2D collider)
    {
        Vector3 unallocatedCoordinate = collider.transform.position;
        Vector3 rightCoord = unallocatedCoordinate;
        Vector3 leftCoord = unallocatedCoordinate;

        foreach (Vector3 childCoordinate in coordinate.GetCoodinate())
        {
            if (childCoordinate.x <= unallocatedCoordinate.x && rightCoord != childCoordinate)
            {
                rightNeighbours.Add(childCoordinate);
                //leftCoord = unallocatedCoordinate; ?????
            }
            else if (childCoordinate.x >= unallocatedCoordinate.x && leftCoord != childCoordinate)
            {
                leftNeighbours.Add(unallocatedCoordinate);
                //leftCoord = unallocatedCoordinate; ????
            }
        }
    }
    private void RemoveNeighborsCoordinates(Collider2D collider)
    {
        rightNeighbours.RemoveAll(i => i == collider.transform.position);
        leftNeighbours.RemoveAll(i => i == collider.transform.position);
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
       GameObject currentTetromino = Instantiate(RandomFigureSelection(this.tetrominoCollection), figureSpawnPosition, Quaternion.identity);
        OnCreateTetrominoEvent?.Invoke(currentTetromino);
    }
    void Test(GameObject asd)
    {

    }

    private void Start()
    {

        
    }

    private void FixedUpdate()
    {
        PatroDetector(this.detectorStartPosition);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetNeighborsCoordinates(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveNeighborsCoordinates(collision);
    }


}
