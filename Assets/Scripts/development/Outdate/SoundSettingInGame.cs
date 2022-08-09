using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSettingInGame : MonoBehaviour
{
    public AudioMixerGroup Group;
    public GameObject Music;
    public GameObject BackMusic;
    private void Start()
    {
        BackMusic.GetComponent<AudioSource>().mute = PlayerPrefs.GetInt("MusicEnabled") == 1;
        Music.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MasterVolume");
        BackMusic.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MasterVolume");
    }
}
