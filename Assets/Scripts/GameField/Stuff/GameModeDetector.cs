using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeDetector
{
    Scene currentScene=SceneManager.GetActiveScene();

    public enum GameMode
    {
        PhysicsOne=1,
        PhysicsTwo=2,
        MatrixClassic=3,
        MatrixTreeInRow=4
    }

    private Dictionary<string, GameMode> sceneGameModeMap = new Dictionary<string, GameMode>()
    {
        ["PhisicOne"]=GameMode.PhysicsOne, 
        ["PhisicTwo"]=GameMode.PhysicsTwo,
        ["MatrixOne"]=GameMode.MatrixClassic,
        ["MatrixTwo"]=GameMode.MatrixTreeInRow
    };

    private string GetSceneName()
    {
        return currentScene.name;
    }
    private bool IsContainsSceneName(string name)
    {
        return sceneGameModeMap.ContainsKey(name);
    }
    public GameMode GetGameMode()
    {
        string sceneName = GetSceneName();
        if (!IsContainsSceneName(sceneName))
            throw new Exception($"current {sceneName} dont contains gamemode");
        return sceneGameModeMap[sceneName];
    }
}
