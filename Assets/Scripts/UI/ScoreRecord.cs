using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
class ScoreRecord : MonoBehaviour
{
    [SerializeField]
    private GameMode gameMode;
    [SerializeField]
    private TextMeshProUGUI recordTextBox;

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
        if(gameMode==GameMode.PhysicsTwo)
            return ScorePhysics.GetSavedScore(ScorePhysics.PhysicsMode.PhysicsTwo);
        if(gameMode==GameMode.Classic)
            return ScoreClassic.GetSavedScore();
        if(gameMode==GameMode.TreeInRow)
            return ScoreTreeInRow.GetSavedScore();
        throw new Exception("no mode exists");
    }

    private void Start()
    {
        int record= GetSavedScore(gameMode);
        if (record == 0)
            this.gameObject.SetActive(false);
        recordTextBox.text =$"{record}";
    }
}
