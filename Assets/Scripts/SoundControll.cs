using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundControll : MonoBehaviour
{
    public AudioMixerGroup Group;
    public GameObject Music;
    public GameObject BackMusic;
    private void Start()
    {
        Music.GetComponentInChildren<Toggle>().isOn = PlayerPrefs.GetInt("MusicEnabled") == 1;
        Music.GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("MasterVolume");
    }
    public void MusicOn(bool enabled)
    {
        if (enabled)
        {
            Group.audioMixer.SetFloat("Music", 0);
        }
        else
        {
            Group.audioMixer.SetFloat("Music", -80);
        }
        PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
    }
    public void MasterVolume(float level)
    {
        /*Group.audioMixer.SetFloat("Master", Mathf.Lerp(-80,0, level));*/
        
        GetComponent<AudioSource>().volume = level;
        BackMusic.GetComponent<AudioSource>().volume = level;
        PlayerPrefs.SetFloat("MasterVolume", level);
    }
}
