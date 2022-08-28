using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Score
{
    public int Point { get; private set; }
    private protected int cellCombinations;


    /*    private Dictionary<GameMode, string> saveLvlSccoreMap = new Dictionary<GameMode, string>()
        {
            [GameMode.PhysicsOne]="ScoreFirstMode", 
            [GameMode.PhysicsTwo]="ScoreSecondMode",
            [GameMode.MatrixClassic]="ScoreThirdMode",
            [GameMode.MatrixTreeInRow]="ScoreFourthMode"
        };
    */
    /*   public Score(int cellCombinations, int raycastOnLine, GameMode gameMode)
       {
           if (gameMode.GetHashCode() > 2)
               throw new Exception("Mod can only be Physics...");

           this.cellCombinations = cellCombinations;
           this.raycastOnLine = raycastOnLine;
       }
       public Score(int cellCombinations,  GameMode gameMode)
       {
           if (gameMode.GetHashCode() < 3)
               throw new Exception("Mod can only be Matrix...");

           this.cellCombinations = cellCombinations;
       }
       public Score(int cellCombinations)
       {
           this.cellCombinations = cellCombinations;
       }
   */
    private protected abstract void SaveScore();
    private protected abstract int GetSavedScore();
    private protected abstract int GetCombo();

    private protected void AddPoint(int points)
    {
        Point += points;
        BusEvent.OnAddScoreEvent?.Invoke(Point);
    }
}

