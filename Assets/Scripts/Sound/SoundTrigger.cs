using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class SoundTrigger: MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField]
    private AudioClip lose;
    [SerializeField]
    private AudioClip falled;
    [SerializeField]
    private AudioClip destroing;
    [SerializeField]
    private AudioClip switching;

    private AudioSource audioSource;

    private void OnDeleteTetromino(GameObject _)
    {
        audioSource.PlayOneShot(falled);
    }

    private void OnLoseGame()
    {
        audioSource.PlayOneShot(lose);
    }

    private void OnSwitchTeromino()
    {
        audioSource.PlayOneShot(switching);
    }
    private void OnStartDestroyAnimation()
    {
        audioSource.PlayOneShot(destroing);
    }

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
        BusEvent.OnLoseGameEvent+=OnLoseGame;
        BusEvent.OnSwitchTetrominoEvent+=OnSwitchTeromino;
        BusEvent.OnStartDestroyAnimationEvent += OnStartDestroyAnimation;
    }
    private void OnDisable()
    {
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
        BusEvent.OnLoseGameEvent-=OnLoseGame;
        BusEvent.OnSwitchTetrominoEvent-=OnSwitchTeromino;
        BusEvent.OnStartDestroyAnimationEvent -= OnStartDestroyAnimation;
    }
}
