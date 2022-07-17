using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public GameObject WelcomePanel,InGame,Pause,Lose,StartGame;
    public void OnPlayButtonPressed() //начинает игровой цикл после попадания на сцену
    {
        StartGame.SetActive(false);
        InGame.SetActive(true);
    }
    public void OnPauseButtonPressed() //приостанавливает текущие процессы до возобновления по кнопке придолжить
    {
        InGame.SetActive(false);
        Pause.SetActive(true);
    }
    public void OnRestartButtonPressed() //перезапуск сцены
    {

    }
    public void OnMenuButtonPressed() //загрузка сцены меню
    {

    }
    public void OnContinueButtonPressed() // возобновление процесса игры 
    {
        Pause.SetActive(false);
        InGame.SetActive(true);
    }
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(1);
    }
}
