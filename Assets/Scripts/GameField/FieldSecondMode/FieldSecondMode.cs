class FieldSecondMode : FieldBase
{
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
        BusEvent.OnKeyDownEvent -= SwitchTetromino;
    }

}
