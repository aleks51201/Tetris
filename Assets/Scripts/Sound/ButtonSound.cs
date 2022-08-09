using UnityEngine;

internal class ButtonSound : MonoBehaviour
{
    private void FixedUpdate()
    {
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MasterVolume");
    }
}
