using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public GameObject welcomePanel, inGame, pause, lose, startGame, gameField;
    ICreatable createFigure;
    IPauseable pauseGane;

    public void OnPlayButtonPressed() //начинает игровой цикл после попадания на сцену
    {
        startGame.SetActive(false);
        inGame.SetActive(true);
        
        if (createFigure != null)
            createFigure.Create();
    }
    public void OnPauseButtonPressed() //приостанавливает текущие процессы до возобновления по кнопке придолжить
    {

        inGame.SetActive(false);
        pause.SetActive(true);
        if (pauseGane != null)
            pauseGane.PauseGame();
    }
    public void OnRestartButtonPressed() //перезапуск сцены
    {
        SceneTransition.SwitchScene("PhisicOne");
    }
    public void OnMenuButtonPressed() //загрузка сцены меню
    {
         SceneTransition.SwitchScene("Menu");
    }
    public void OnContinueButtonPressed() // возобновление процесса игры 
    {
        pause.SetActive(false);
        inGame.SetActive(true);
        if (pauseGane != null)
            pauseGane.ContinueGame();
    }
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(1);
    }
    private void Start()
    {
         createFigure = gameField.GetComponent<ICreatable>();
        pauseGane = gameField.GetComponent<IPauseable>();
    }
}
