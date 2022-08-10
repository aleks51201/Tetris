using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
internal class ButtonSound : MonoBehaviour
{
    private AudioSource audioSource;
    private Coroutine coroutine;

    public void OnStartSound()
    {
        coroutine = StartCoroutine(Unmute());
    }
    private IEnumerator Unmute()
    {
        yield return new WaitForSeconds(0.01f);
        audioSource.mute = false;
    }

    private void FixedUpdate()
    {
        audioSource.volume = PlayerPrefs.GetFloat("MasterVolume");
    }

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
        BusEvent.OnStartSoundEvent += OnStartSound;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
        BusEvent.OnStartSoundEvent -= OnStartSound;
    }
}
