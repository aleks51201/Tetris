using UnityEngine;

class Score
{
    public int Point { get; private set; }
    private Vector2 startPosition;

    public Score()
    {
        BusEvent.OnStartAccelerationEvent += OnStartAcceleration;
        BusEvent.OnEndAccelerationEvent += OnEndAcceleration;
    }

    public void AddPoint(int points)
    {
        this.Point += points;
        BusEvent.OnAddScoreEvent?.Invoke(this.Point);
    }
    private void OnStartAcceleration(Vector2 startPosition)
    {
        this.startPosition = startPosition;
    }

    private void OnEndAcceleration(Vector2 endPosition)
    {
        int distance = (int)DistanceCalculation(this.startPosition, endPosition);
        AddPoint(distance);
    }
    private float DistanceCalculation(Vector2 startPosition, Vector2 endPosition)
    {
        return (startPosition.y - endPosition.y) * 1;
    }

}

