using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundControll : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup Group;
    [SerializeField]
    private GameObject Music;
    [SerializeField]
    private GameObject BackMusic;

    private void Start()
    {
        Music.GetComponentInChildren<Toggle>().isOn = PlayerPrefs.GetInt("MusicEnabled") == 1;
        Music.GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("MasterVolume");
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
        GetComponent<AudioSource>().volume = level;
        BackMusic.GetComponent<AudioSource>().volume = level;
        PlayerPrefs.SetFloat("MasterVolume", level);
    }
}
