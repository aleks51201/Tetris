using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SoundControll : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup Group;
    [SerializeField]
    private GameObject BackMusic;

    private Slider masterVolumeSlider;
    private Toggle masterVolumeToggle;
    private FindSoundObject findSoundObject;


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
        this.BackMusic.GetComponent<AudioSource>().volume = level;
        PlayerPrefs.SetFloat("MasterVolume", level);
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
        Toggle toggle= findSoundObject.TryFindMasterVolumeToggle();
        if (toggle != null)
            this.masterVolumeToggle = toggle;
        Slider slider = findSoundObject.TryFindMasterVolumeSlider();
        if (slider!= null)
            this.masterVolumeSlider= slider;
        RegisterCallbackEvent(true);
    }

    private void OnSceneSwitch()
    {
        OnStartScene();
        this.masterVolumeToggle.isOn = PlayerPrefs.GetInt("MusicEnabled") == 1;
        this.masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
    }

    private void RegisterCallbackEvent(bool register)
    {
        if (register)
        {
            this.masterVolumeSlider.onValueChanged.AddListener(delegate { SliderChangeEvent(); });
            this.masterVolumeToggle.onValueChanged.AddListener(delegate { ToggleChangeEvent(); });
            return;
        }
        this.masterVolumeToggle.onValueChanged.RemoveListener(delegate { ToggleChangeEvent(); });
        this.masterVolumeSlider.onValueChanged.RemoveListener(delegate { SliderChangeEvent(); });
    }
    private void Start()
    {
        findSoundObject = new();
        OnSceneSwitch();
    }

    private void OnEnable()
    {
        BusEvent.OnSceneSwitchEvent += OnSceneSwitch;
    }

    private void OnDisable()
    {
        BusEvent.OnSceneSwitchEvent -= OnSceneSwitch;
        RegisterCallbackEvent(false);
    }
}
