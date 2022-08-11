using UnityEngine;

internal class Score
{
    public int Point { get; private set; }
    private Vector2 startPosition;
    private int cellCombinations;
    private int raycastOnLine;

    private enum GameMode
    {
        Physics = 1,
        MatrixClassic = 2,
        MatrixTreeInRow = 3,
    }

    public Score(int cellCombinations, int raycastOnLine)
    {
        this.cellCombinations = cellCombinations;
        this.raycastOnLine = raycastOnLine;
        BusEvent.OnStartAccelerationEvent += OnStartAcceleration;
        BusEvent.OnEndAccelerationEvent += OnEndAcceleration;
    }

    private void AddPoint(int points)
    {
        Point += points;
        BusEvent.OnAddScoreEvent?.Invoke(Point);
    }

    private void OnStartAcceleration(Vector2 startPosition)
    {
        this.startPosition = startPosition;
    }

    private void OnEndAcceleration(Vector2 endPosition)
    {
        int distance = (int)DistanceCalculation(startPosition, endPosition);
        AddPoint(distance);
    }

    private float DistanceCalculation(Vector2 startPosition, Vector2 endPosition)
    {
        return (startPosition.y - endPosition.y) * 1;
    }
    public void AddPointPhysicsMode(RaycastHit2D[] detectionLines)
    {
        int[] scoreSystem = { 40,100,300,1200};
        int lineCoef = raycastOnLine / cellCombinations;
        int detectedObjects = detectionLines.Length;
        int cells = detectedObjects / lineCoef;
        int lines = cells / cellCombinations;
        int points = scoreSystem[lines-1];
        AddPoint(points);
    }
}

