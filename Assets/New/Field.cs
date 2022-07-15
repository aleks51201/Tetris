using System.Collections.Generic;
using UnityEngine;
using System;


public class Field : MonoBehaviour, ICreatable
{
    [Header("Teromino list")]
    public GameObject[] tetrominoCollection;

    [Header("Figure start position")]
    public Vector2 figureSpawnPosition;
    

    [Header("Detector start position")]
    public Vector3 detectorStartPosition;

    [Header("Layer lvl")]
    public LayerMask maskName;

    [Header("Field size")]
    private int widthField = 10;
    private int heightField = 20;

    [SerializeField]
    private Vector2 figureQueueSpawnPosition;
    [SerializeField]
    private int sizeQueue;


   // private GameObject currentTetromino;
    private QueueField queueField;




    private RaycastHit2D[] GetDetectionObject(Vector3 detectorPosition)
    {
        return Physics2D.RaycastAll(detectorPosition, Vector2.right, this.widthField, maskName);
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
    
    public void Create()
    {
        GameObject currentTetromino = Instantiate(RandomFigureSelection(this.tetrominoCollection), figureQueueSpawnPosition, Quaternion.identity);
        queueField.AddObject(currentTetromino);
        BusEvent.OnCreateTetrominoEvent?.Invoke(currentTetromino);
    }
    private void OnDeleteTetromino()
    {
        SpawnTetromino();
        BusEvent.OnAddObjectToQueueEvent -= Create;
        BusEvent.OnQueueFullEvent -= OnDeleteTetromino;
    }
    private void SpawnTetromino()
    {
        Create();
        GameObject tetromino = queueField.queueOfTetromino.Dequeue();
        BusEvent.OnSpawnTetrominoEvent?.Invoke(tetromino);
        tetromino.transform.position = figureSpawnPosition;

    }

    private void OnDeleteTetromino2(GameObject fig)
    {
        SpawnTetromino();
    }
    public bool IsLose(Collider2D collider)
    {
        return collider.CompareTag("Figure");
    }

    private void FixedUpdate()
    {
        PatroDetector(this.detectorStartPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Figure"))
        {
            Debug.Log("end");
            BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino2;
        }
    }
    private void Start()
    {
        queueField = new QueueField(sizeQueue);
        Create();
    }
    private void OnEnable()
    {
        BusEvent.OnAddObjectToQueueEvent += Create;
        BusEvent.OnQueueFullEvent += OnDeleteTetromino;
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino2;

    }
    private void OnDisable()
    {

        

    }
}
