using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public GameObject welcomePanel, inGame, pause, lose, startGame, gameField;
    ICreatable createFigure;
    IPauseable pauseGane;
    private string currentSceneName;

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
        OnButtonClicked(true);
    }
    public void OnRestartButtonPressed() //перезапуск сцены
    {
        SceneTransition.SwitchScene(currentSceneName);
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
        OnButtonClicked(false);
    }
    private void OnButtonClicked(bool isPaused)
    {
        //ProjectContext.Instance.PauseManager.SetPaused(isPaused);
        BusEvent.OnPauseEvent?.Invoke(isPaused);
    }
    private void OnLose()
    {
        inGame.GetComponent<Animator>().SetTrigger("InGameDisappear");
    }
    private void OnPause(KeyCode keyCode,float _)
    {
        if (keyCode != KeyCode.Escape)
            return;
        OnPauseButtonPressed();
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
        welcomePanel.GetComponent<Animator>().SetTrigger("AfterLoad");
        currentSceneName = SceneManager.GetActiveScene().name;
    }
    private void OnEnable()
    {
        BusEvent.OnLoseGameEvent += OnLose;
        BusEvent.OnKeyDownEvent += OnPause;
    }
    private void OnDisable()
    {
        BusEvent.OnLoseGameEvent -= OnLose;
        BusEvent.OnKeyDownEvent -= OnPause;
    }
}
