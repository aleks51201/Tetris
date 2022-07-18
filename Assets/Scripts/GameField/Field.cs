using UnityEngine;


public class Field : MonoBehaviour, ICreatable, ISwitchTetromino, IPauseable 
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
    [SerializeField]
    private float queueShift;
    [SerializeField]
    private Vector2 figureStashSpawnPosition;
    

    private QueueField queueField;
    private Stash gameStash;
    private GameObject currentTetrominoInGame;
    private Score gameScore = new();




    private RaycastHit2D[] GetDetectionObject(Vector3 detectorPosition)
    {
        return Physics2D.RaycastAll(detectorPosition, Vector2.right, this.widthField, maskName);
    }

    private void PatroDetector(Vector2 detectorStartPosition)
    {
        Vector2 newPosition;
        int numLine = 0;
        for (int i = 0; i < this.heightField; i++)
        {
            newPosition = detectorStartPosition + new Vector2(0, i);
            if (GetDetectionObject(newPosition).Length == 0)
                break;
            RaycastHit2D[] detectedObject = GetDetectionObject(newPosition);
            CheckLineFull(detectedObject);
            if (isLineFull(detectedObject))
                numLine++;
        }
        gameScore.AddPoint(numLine * 100);
    }
    private bool isLineFull(RaycastHit2D[] detectedObject)
    {
        if (detectedObject.Length >= widthField * 2)
            return true;
        else
            return false;
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
    private void OnQueueFull()
    {
        SpawnTetromino();
        BusEvent.OnAddObjectToQueueEvent -= Create;
        BusEvent.OnQueueFullEvent -= OnQueueFull;
    }
    private void SpawnTetromino()
    {
        Create();
        this.currentTetrominoInGame = queueField.queueOfTetromino.Dequeue();
        BusEvent.OnSpawnTetrominoEvent?.Invoke(this.currentTetrominoInGame);
        this.currentTetrominoInGame.transform.position = figureSpawnPosition;
        Debug.Log("Score: "+gameScore.point);
    }
    public void SwitchTetromino()
    {
        if (gameStash.isItSwithcable) 
        {
            GameObject newTetromino = gameStash.SwitchTetromino(this.currentTetrominoInGame);
            this.currentTetrominoInGame = newTetromino;
            if (newTetromino == null)
                SpawnTetromino();
        }
    }

    private void OnDeleteTetromino(GameObject fig)
    {
        SpawnTetromino();
    }
    private void OnLoseGame()
    {
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
    }
    public bool IsLose(Collider2D collider)
    {
        return collider.CompareTag("Figure");
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        PatroDetector(this.detectorStartPosition);
    }

    private void Start()
    {
        queueField = new QueueField(sizeQueue, queueShift);
       
        gameStash = new Stash(figureStashSpawnPosition, figureSpawnPosition);
    }
    private void OnEnable()
    {
        BusEvent.OnAddObjectToQueueEvent += Create;
        BusEvent.OnQueueFullEvent += OnQueueFull;
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
        BusEvent.OnLoseGameEvent += OnLoseGame;
    }
    private void OnDisable()
    {
        BusEvent.OnLoseGameEvent -= OnLoseGame;

        BusEvent.OnAddObjectToQueueEvent -= Create;
        BusEvent.OnQueueFullEvent -= OnQueueFull;
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        BusEvent.OnLoseGameEvent -= OnLoseGame;
    }

}
