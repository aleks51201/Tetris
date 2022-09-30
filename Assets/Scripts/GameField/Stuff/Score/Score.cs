public abstract class Score
{
    public int Point { get; private set; }
    private protected int cellCombinations;

    private protected abstract void SaveScore();
    private protected abstract int GetCombo();

    private protected void AddPoint(int points)
    {
        Point += points;
        BusEvent.OnAddScoreEvent?.Invoke(Point);
    }
}

