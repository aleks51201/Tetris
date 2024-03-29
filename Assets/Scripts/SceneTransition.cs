using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Animator componentAnimator;
    private AsyncOperation loadingSceneOperation;
    private static SceneTransition instance;

    public static void SwitchScene(string sceneName)
    {
        instance.componentAnimator.SetTrigger("OnLoad");
        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.loadingSceneOperation.allowSceneActivation = false;
    }

    void Start()
    {
        instance = this;
        componentAnimator = GetComponent<Animator>();
    }

    public void OnAnimationOver()
    {
        loadingSceneOperation.allowSceneActivation = true;
        loadingSceneOperation.completed+=OnSceneLoadCompleted;
    }
    private void OnSceneLoadCompleted(AsyncOperation value)
    {
        BusEvent.OnSceneSwitchEvent?.Invoke();
    }
}
