
using UnityEngine;

public class ProjectContext:MonoBehaviour
{
    public PauseManager PauseManager { get; private set; }
    public static ProjectContext Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        PauseManager = new PauseManager();
    }
}
