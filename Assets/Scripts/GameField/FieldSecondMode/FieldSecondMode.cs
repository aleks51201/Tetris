using UnityEngine;

internal class FieldSecondMode : FieldBase
{
    [Header("Line detector")]
    [SerializeField]
    private protected Vector2 lineDetectorPosition;
    [SerializeField]
    private protected LayerMask maskName;
    [SerializeField]
    private protected int numObjectOnLine;

    private protected override void SpawnTetromino()
    {
        Create();
        this.currentTetrominoInGame = gameQueue.queueOfTetromino.Dequeue();
        BusEvent.OnSpawnTetrominoEvent?.Invoke(this.currentTetrominoInGame);
        this.currentTetrominoInGame.transform.position = spawnPosition;
    }
    private protected void Scoring(RaycastHit2D[] detectedObjects)
    {
        gameScore.AddPoint(detectedObjects.Length / numObjectOnLine * 100);
    }
    private protected override void IsPaused(bool isPaused)
    {
        Rigidbody2D tetromino = this.currentTetrominoInGame.GetComponent<Rigidbody2D>();
        Vector2 currentVelocity = tetromino.velocity;
        if (isPaused)
        {
            tetromino.isKinematic = true;
            tetromino.velocity = new Vector2(0, 0);
        }
        else
        {
            tetromino.isKinematic = false;
            tetromino.velocity = currentVelocity;
        }
    }

    private void Update()
    {
    }
    private void FixedUpdate()
    {
        gameLineDetector.PatrolDetector(lineDetectorPosition);
    }
    private void Start()
    {
        gameStash = new(stashPosition, spawnPosition);
        gameQueue = new(queueSize, queueShift);
        gameScore = new();
        gameLineDetector = new(this.fieldHeight, this.fieldWidth, this.maskName, this.numObjectOnLine);
    }

    private void OnEnable()
    {
        BusEvent.OnAddObjectToQueueEvent += Create;
        BusEvent.OnQueueFullEvent += OnQueueFull;
        BusEvent.OnAddScoreEvent += OnAddScore;
        BusEvent.OnLoseGameEvent += OnLoseGame;
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
        BusEvent.OnPauseEvent += IsPaused;
        BusEvent.OnLineIsFullEvent += StartDestroyAnimation;
        BusEvent.OnLineIsFullEvent += Scoring;
        BusEvent.OnKeyDownEvent += SwitchTetromino;

    }
    private void OnDisable()
    {
        BusEvent.OnAddObjectToQueueEvent -= Create;
        BusEvent.OnQueueFullEvent -= OnQueueFull;
        BusEvent.OnAddScoreEvent -= OnAddScore;
        BusEvent.OnLoseGameEvent -= OnLoseGame;
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        BusEvent.OnPauseEvent -= IsPaused;
        BusEvent.OnLineIsFullEvent -= StartDestroyAnimation;
        BusEvent.OnLineIsFullEvent -= Scoring;
        BusEvent.OnKeyDownEvent -= SwitchTetromino;
    }

}
