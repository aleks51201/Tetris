using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SoundControll : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup Group;
    [SerializeField]
    private GameObject Music;
    [SerializeField]
    private GameObject BackMusic;

    private Scene currentScene;
    private Slider masterVolumeSlider;
    private Toggle masterVolumeToggle;

    private void Start()
    {
        OnStartScene();
        this.masterVolumeToggle.isOn=PlayerPrefs.GetInt("MusicEnabled")==1;
        this.masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
    }

    public void MusicOn(bool enabled)
    {
        if (enabled)
            Group.audioMixer.SetFloat("Music", 0);
        else
            Group.audioMixer.SetFloat("Music", -80);
        PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
    }

    public void MasterVolume(float level)
    {
        this.GetComponent<AudioSource>().volume = level;
        BackMusic.GetComponent<AudioSource>().volume = level;
        PlayerPrefs.SetFloat("MasterVolume", level);
    }

    private void SetScene()
    {
        this.currentScene = SceneManager.GetActiveScene();
    }

    private GameObject TryFindCanvasOnScene()
    {
        GameObject[] inSceneObjects;
        inSceneObjects = this.currentScene.GetRootGameObjects();
        foreach (GameObject inSceneObject in inSceneObjects)
        {
            if (inSceneObject.name == "Canvas")
                return inSceneObject;
        }
        return null;
    }

    private Slider TryFindMasterVolumeSlider(GameObject canvas)
    {
        Slider[] childObjects = canvas.GetComponentsInChildren<Slider>(includeInactive:true);
        foreach (Slider childObject in childObjects)
        {
            if (childObject.name == "Slider")
                return childObject;
        }
        return null;
    }

    private Toggle TryFindMasterVolumeToggle(GameObject canvas)
    {
        Toggle[] childObjects = canvas.GetComponentsInChildren<Toggle>(includeInactive:true);
        foreach (Toggle childObject in childObjects)
        {
            if (childObject.name == "Toggle")
                return childObject;
        }
        return null;
    }

    private void SliderChangeEvent()
    {
        MasterVolume(this.masterVolumeSlider.value);
    }

    private void ToggleChangeEvent()
    {
        MusicOn(this.masterVolumeToggle.isOn);
    }

    private void OnStartScene()
    {
        SetScene();
        GameObject canvas = TryFindCanvasOnScene() ;
        if (canvas == null)
            return;
        if (TryFindMasterVolumeSlider(canvas) == null)
            return; 
        this.masterVolumeSlider=TryFindMasterVolumeSlider(canvas);
        if (TryFindMasterVolumeToggle(canvas)==null)
            return; 
        this.masterVolumeToggle=TryFindMasterVolumeToggle(canvas);
        RegisterCallbackEvent();
    }
    private void OnSceneSwitch()
    {
        OnStartScene();
        Debug.Log(this.currentScene.name);
        this.masterVolumeToggle.isOn=PlayerPrefs.GetInt("MusicEnabled")==1;
        this.masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
    }

    private void RegisterCallbackEvent()
    {
        this.masterVolumeSlider.onValueChanged.AddListener(delegate { SliderChangeEvent(); });
        this.masterVolumeToggle.onValueChanged.AddListener(delegate { ToggleChangeEvent(); });
    }

    private void OnEnable()
    {
        BusEvent.OnSceneSwitchEvent += OnSceneSwitch;
    }

    private void OnDisable()
    {
        BusEvent.OnSceneSwitchEvent -= OnSceneSwitch;
    }
}
