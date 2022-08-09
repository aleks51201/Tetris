using UnityEngine;
using UnityEngine.SceneManagement;

internal class Sound : MonoBehaviour
{
    private static bool isLoaded = false;

    private void Awake()
    {
        StatusCheck();
    }

    private void StatusCheck()
    {
        if (isLoaded)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        isLoaded = true;
    }
    
}
