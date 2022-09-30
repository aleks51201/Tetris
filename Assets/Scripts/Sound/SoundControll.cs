using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundControll : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup audioMixer;
    [SerializeField]
    private GameObject backMusic;

    private Slider masterVolumeSlider;
    private Toggle masterVolumeToggle;
    private FindSoundObject findSoundObject;

    public void SaveVolumeStateBackgroundMusic(bool enabled)
    {
        PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
    }

    private bool GetSavedVolumeStateBackgroundMusic()
    {
        return PlayerPrefs.GetInt("MusicEnabled") == 1;
    }

    private void SwitchBackgroundMusicVolume(bool enabled)
    {
        if (enabled)
            audioMixer.audioMixer.SetFloat("Music", 0);
        else
            audioMixer.audioMixer.SetFloat("Music", -80);
    }

    public void MasterVolume(float level)
    {
        this.GetComponent<AudioSource>().volume = level;
        backMusic.GetComponent<AudioSource>().volume = level;
        PlayerPrefs.SetFloat("MasterVolume", level);
    }

    private void SliderChangeEvent()
    {
        MasterVolume(masterVolumeSlider.value);
    }

    private void ToggleChangeEvent()
    {
        SwitchBackgroundMusicVolume(masterVolumeToggle.isOn);
        SaveVolumeStateBackgroundMusic(masterVolumeToggle.isOn);
    }

    private void OnStartScene()
    {
        Toggle toggle = findSoundObject.TryFindMasterVolumeToggle();
        if (toggle != null)
            masterVolumeToggle = toggle;
        Slider slider = findSoundObject.TryFindMasterVolumeSlider();
        if (slider != null)
            masterVolumeSlider = slider;
        RegisterCallbackEvent();
    }

    private void OnSceneSwitch()
    {
        OnStartScene();
        masterVolumeToggle.isOn = PlayerPrefs.GetInt("MusicEnabled") == 1;
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        BusEvent.OnStartSoundEvent?.Invoke();
    }

    private void RegisterCallbackEvent()
    {
        masterVolumeToggle.onValueChanged.AddListener(delegate { ToggleChangeEvent(); });
        masterVolumeSlider.onValueChanged.AddListener(delegate { SliderChangeEvent(); });
    }

    private void OnLoseGame()
    {
        SwitchBackgroundMusicVolume(false);
    }
    private void OnRestartButtonClick()
    {
        SwitchBackgroundMusicVolume(GetSavedVolumeStateBackgroundMusic());
    }

    private void Start()
    {
        findSoundObject = new();
        OnSceneSwitch();
        SwitchBackgroundMusicVolume(masterVolumeToggle.isOn);
        SaveVolumeStateBackgroundMusic(masterVolumeToggle.isOn);
    }

    private void OnEnable()
    {
        BusEvent.OnSceneSwitchEvent += OnSceneSwitch;
        BusEvent.OnLoseGameEvent += OnLoseGame;
        BusEvent.OnRestartButtonClickEvent += OnRestartButtonClick;
    }

    private void OnDisable()
    {
        BusEvent.OnSceneSwitchEvent -= OnSceneSwitch;
        BusEvent.OnLoseGameEvent -= OnLoseGame;
        BusEvent.OnRestartButtonClickEvent -= OnRestartButtonClick;
    }
}
