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
        StartCoroutine(TurnOff(startGame, 0.55f));
        startGame.GetComponent<Animator>().SetTrigger("StartDisappear");
        inGame.SetActive(true);
        
        if (createFigure != null)
            createFigure.Create();
    }
    public void OnPauseButtonPressed() //приостанавливает текущие процессы до возобновления по кнопке придолжить
    {
        StartCoroutine(TurnOff(inGame, 0.75f));
        inGame.GetComponent<Animator>().SetTrigger("InGameDisappear");
        pause.SetActive(true);
        if (pauseGane != null)
            pauseGane.PauseGame();
    }
    public void OnRestartButtonPressed() //перезапуск сцены
    {
        SceneTransition.SwitchScene("PhisicOne");
        if (pauseGane != null)
            pauseGane.ContinueGame();
    }
    public void OnMenuButtonPressed() //загрузка сцены меню
    {
         SceneTransition.SwitchScene("Menu");  //дёргать после анимации 0.75f секунды
        if (pauseGane != null)
            pauseGane.ContinueGame();
    }
    public void OnContinueButtonPressed() // возобновление процесса игры 
    {
        StartCoroutine(TurnOff(pause, 0.75f));
        pause.GetComponent<Animator>().SetTrigger("PauseDisappear");
        inGame.SetActive(true);
        if (pauseGane != null)
            pauseGane.ContinueGame();
    }

    IEnumerator TurnOff(GameObject Who, float Time)
    {
        yield return new WaitForSeconds(Time);
        Who.SetActive(false);
    }
    private void Start()
    {
         createFigure = gameField.GetComponent<ICreatable>();
        pauseGane = gameField.GetComponent<IPauseable>();
        //StartCoroutine(TurnOff(welcomePanel, 0.75f));
        welcomePanel.GetComponent<Animator>().SetTrigger("AfterLoad");
    }
}
