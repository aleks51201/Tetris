using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FindSoundObject
{
    private Scene currentScene;

    private void SetScene()
    {
        this.currentScene = SceneManager.GetActiveScene();
    }

    private GameObject TryFindCanvasOnScene()
    {
        SetScene();
        GameObject[] inSceneObjects;
        inSceneObjects = this.currentScene.GetRootGameObjects();
        foreach (GameObject inSceneObject in inSceneObjects)
        {
            if (inSceneObject.name == "Canvas")
                return inSceneObject;
        }
        return null;
    }

    public Slider TryFindMasterVolumeSlider()
    {
        GameObject canvas = TryFindCanvasOnScene();
        if (canvas == null)
            return null;
        Slider[] childObjects = canvas.GetComponentsInChildren<Slider>(includeInactive: true);
        foreach (Slider childObject in childObjects)
        {
            if (childObject.name == "Slider")
                return childObject;
        }
        return null;
    }

    public Toggle TryFindMasterVolumeToggle()
    {
        GameObject canvas = TryFindCanvasOnScene();
        if (canvas == null)
            return null;
        Toggle[] childObjects = canvas.GetComponentsInChildren<Toggle>(includeInactive: true);
        foreach (Toggle childObject in childObjects)
        {
            if (childObject.name == "Toggle")
                return childObject;
        }
        return null;
    }

}
