using System.Collections.Generic;
using UnityEngine;

public class ScoreTreeInRow : Score
{
    private bool tetris = false;

    public ScoreTreeInRow(int cellCombinations)
    {
        this.cellCombinations = cellCombinations;
    }

    private protected override void SaveScore()
    {
        if (Point < GetSavedScore())
            return;
        PlayerPrefs.SetInt("TreeInRowScore", Point);
    }

    public static int GetSavedScore()
    {
        return PlayerPrefs.GetInt("TreeInRowScore");
    }

    private void TetrisCheck(int lines)
    {
        tetris = lines > 1;
    }

    private protected override int GetCombo()
    {
        if (tetris)
            return 2;
        return 1;
    }

    public void CalcPoint(List<Transform> detectionLines)
    {
        int scoreSystem = 30;
        int cells = detectionLines.Count;
        int lines = cells / cellCombinations;
        TetrisCheck(lines);
        int combo = lines * GetCombo();
        int points = scoreSystem ^ combo;
        AddPoint(points);
        SaveScore();
    }
}
