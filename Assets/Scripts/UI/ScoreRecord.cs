﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
class ScoreRecord: MonoBehaviour
{
    [SerializeField]
    private GameMode gameMode;
    [SerializeField]
    private TextMeshProUGUI text;

    enum GameMode 
    {
        PhysicsOne=1,
        PhysicsTwo=2,
        Classic=3,
        TreeInRow=4
    }

    private int GetSavedScore(GameMode gameMode)
    {
        if(gameMode==GameMode.PhysicsOne)
            return ScorePhysics.GetSavedScore(ScorePhysics.PhysicsMode.PhysicsOne);
        else if(gameMode==GameMode.PhysicsTwo)
            return ScorePhysics.GetSavedScore(ScorePhysics.PhysicsMode.PhysicsTwo);
        else if(gameMode==GameMode.Classic)
            return ScoreClassic.GetSavedScore();
        else if(gameMode==GameMode.TreeInRow)
            return ScoreTreeInRow.GetSavedScore();
        throw new Exception("no mode exists");
    }

    private void Start()
    {
        int salkdj = GetSavedScore(gameMode);
        text.text =$"{GetSavedScore(gameMode)}";
    }
}
