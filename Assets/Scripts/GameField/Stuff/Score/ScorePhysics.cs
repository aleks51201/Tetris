using UnityEngine;

public class ScorePhysics : Score
{
    private bool tetris;
    int raycastOnLine = 0;

    public ScorePhysics(int cellCombinations, int raycastOnLine)
    {
        this.cellCombinations = cellCombinations;
        this.raycastOnLine = raycastOnLine;
    }

    private protected override void SaveScore()
    {
        PlayerPrefs.SetInt("PhysicsScore", Point);
    }

    private protected override int GetSavedScore()
    {
        return PlayerPrefs.GetInt("PhysicsScore");
    }

    private void TetrisCheck(int lines)
    {
        tetris = lines == 4;
    }

    private protected override int GetCombo()
    {
        if (tetris)
            return 2;
        return 1;
    }

    public void CalcPoint(RaycastHit2D[] detectionLines)
    {
        int[] scoreSystem = { 40, 100, 300, 1200 };
        int combo = GetCombo();
        int lineCoef = raycastOnLine / cellCombinations;
        int detectedObjects = detectionLines.Length;
        int cells = detectedObjects / lineCoef;
        int lines = cells / cellCombinations;
        TetrisCheck(lines);
        int points = scoreSystem[lines - 1] * combo;
        AddPoint(points);
        SaveScore();
    }
}
