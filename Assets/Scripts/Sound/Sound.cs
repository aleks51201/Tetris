using UnityEngine;

internal class Sound : MonoBehaviour
{
    private static bool isLoaded = false;

    private void Awake()
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
