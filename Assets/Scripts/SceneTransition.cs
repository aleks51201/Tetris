using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private AsyncOperation loadingSceneOperation;
    private static SceneTransition instance;
    public static void SwitchScene(string sceneName)
    {
        
        //instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        SceneManager.LoadSceneAsync(sceneName);
        //instance.loadingSceneOperation.allowSceneActivation = true;
    }
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
