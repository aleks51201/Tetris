﻿using UnityEngine;

internal class FieldSecondMode : FieldBase
{
    [Header("Line detector")]
    [SerializeField]
    private protected Vector2 lineDetectorPosition;
    [SerializeField]
    private protected LayerMask maskName;
    [SerializeField]
    private protected int numObjectOnLine;
    [Header("Save score")]
    [SerializeField]
    private ScorePhysics.PhysicsMode scorePhysicsMode;

    private protected ScorePhysics gameScore;

    private protected override void SpawnTetromino()
    {
        Create();
        currentTetrominoInGame = gameQueue.queueOfTetromino.Dequeue();
        currentTetrominoInGame.transform.position = spawnPosition;
        BusEvent.OnSpawnTetrominoEvent?.Invoke(currentTetrominoInGame);
    }

    private protected void Scoring(RaycastHit2D[] detectedObjects)
    {
        gameScore.CalcPoint(detectedObjects);
    }

    private void OnLoseGame()
    {
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        losePanel.SetActive(true);
        loseScorePanel.text = $"{gameScore.Point}";
    }

    private void FixedUpdate()
    {
        gameLineDetector.LinePatrol(lineDetectorPosition);
    }

    private void Start()
    {
        gameStash = new(stashPosition, spawnPosition);
        gameQueue = new(queueSize, queueShift);
        gameScore = new(fieldWidth, numObjectOnLine, scorePhysicsMode);
        gameLineDetector = new(fieldHeight, fieldWidth, maskName, numObjectOnLine);
    }

    private void OnEnable()
    {
        BusEvent.OnAddObjectToQueueEvent += Create;
        BusEvent.OnQueueFullEvent += OnQueueFull;
        BusEvent.OnAddScoreEvent += OnAddScore;
        BusEvent.OnLoseGameEvent += OnLoseGame;
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
        //BusEvent.OnPauseEvent += IsPaused;
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
        //BusEvent.OnPauseEvent -= IsPaused;
        BusEvent.OnLineIsFullEvent -= StartDestroyAnimation;
        BusEvent.OnLineIsFullEvent -= Scoring;
        BusEvent.OnKeyDownEvent -= SwitchTetromino;
    }
}
