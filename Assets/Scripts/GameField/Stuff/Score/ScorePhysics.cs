using System;
using UnityEngine;

public class ScorePhysics : Score
{
    private bool tetris;
    private int raycastOnLine = 0;
    private PhysicsMode currentMode;

    public enum PhysicsMode
    {
        PhysicsOne = 1,
        PhysicsTwo = 2,
    }

    public ScorePhysics(int cellCombinations, int raycastOnLine, PhysicsMode mode)
    {
        this.cellCombinations = cellCombinations;
        this.raycastOnLine = raycastOnLine;
        currentMode = mode;
    }

    private protected override void SaveScore()
    {
        if (Point < GetSavedScore(currentMode))
            return;
        if(currentMode == PhysicsMode.PhysicsOne)
            PlayerPrefs.SetInt("PhysicsOneScore", Point);
        if(currentMode == PhysicsMode.PhysicsTwo)
            PlayerPrefs.SetInt("PhysicsTwoScore", Point);
    }

    public static int GetSavedScore(PhysicsMode mode)
    {
        if(mode == PhysicsMode.PhysicsOne)
            return PlayerPrefs.GetInt("PhysicsOneScore");
        else if(mode == PhysicsMode.PhysicsTwo)
            return PlayerPrefs.GetInt("PhysicsTwoScore");
        else
            throw new Exception("no such mod exists.");
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
