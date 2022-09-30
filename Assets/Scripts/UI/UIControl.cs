using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public GameObject welcomePanel, inGame, pause, lose, startGame, gameField;
    private ICreatable createFigure;
    private IPauseable pauseGame;
    private string currentSceneName;

    public void OnPlayButtonPressed()
    {
        startGame.GetComponent<Animator>().SetBool("Next", true);
        inGame.SetActive(true);
        if (createFigure != null)
            createFigure.Create();
    }

    public void OnPauseButtonPressed()
    {
        Animator inGameAnimator = inGame.GetComponent<Animator>();
        inGameAnimator.SetBool("Next", true);
        pause.SetActive(true);
        pause.GetComponent<Animator>().SetBool("Next", false);
        OnButtonClicked(true);
    }

    public void OnRestartButtonPressed()
    {
        SceneTransition.SwitchScene(currentSceneName);
        if (pauseGame != null)
            pauseGame.ContinueGame();
        BusEvent.OnRestartButtonClickEvent?.Invoke();
    }

    public void OnMenuButtonPressed()
    {
        SceneTransition.SwitchScene("Menu");
        if (pauseGame != null)
            pauseGame.ContinueGame();
    }

    public void OnContinueButtonPressed()
    {
        Animator inPauseAnimator = pause.GetComponent<Animator>();
        inPauseAnimator.SetBool("Next", true);
        inGame.SetActive(true);
        inGame.GetComponent<Animator>().SetBool("Next", false);
        OnButtonClicked(false);
    }

    private void OnButtonClicked(bool isPaused)
    {
        BusEvent.OnPauseEvent?.Invoke(isPaused);
    }

    private void OnLose()
    {
        inGame.GetComponent<Animator>().SetTrigger("InGameDisappear");
    }

    private void OnPause(KeyCode keyCode, float _)
    {
        if (keyCode != KeyCode.Escape)
            return;
        OnPauseButtonPressed();
    }

    private void Start()
    {
        createFigure = gameField.GetComponent<ICreatable>();
        pauseGame = gameField.GetComponent<IPauseable>();
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
