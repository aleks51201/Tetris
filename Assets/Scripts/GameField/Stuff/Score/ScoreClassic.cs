using System.Collections.Generic;
using UnityEngine;

public class ScoreClassic : Score
{
    private bool tetris;

    public ScoreClassic(int cellCombinations)
    {
        this.cellCombinations = cellCombinations;
    }

    private protected override void SaveScore()
    {
        if (Point < GetSavedScore())
            return;
        PlayerPrefs.SetInt("ClassicScore", Point);
    }

    public static int GetSavedScore()
    {
        return PlayerPrefs.GetInt("ClassicScore");
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

    public void CalcPoint(List<Transform> detectionLines)
    {
        int[] scoreSystem = { 40, 100, 300, 1200 };
        int combo = GetCombo();
        int cells = detectionLines.Count;
        int lines = cells / cellCombinations;
        TetrisCheck(lines);
        int points = scoreSystem[lines - 1] * combo;
        AddPoint(points);
        SaveScore();
    }
}
