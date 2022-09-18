using System.Collections;
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
        //StartCoroutine(TurnOff(startGame, 0.55f));
        //startGame.GetComponent<Animator>().SetTrigger("StartDisappear");
        startGame.GetComponent<Animator>().SetBool("Next",true);
        inGame.SetActive(true);
        if (createFigure != null)
            createFigure.Create();
    }

    public void OnPauseButtonPressed()
    {
        //StartCoroutine(TurnOff(inGame, 0.75f));
        //inGame.GetComponent<Animator>().SetTrigger("InGameDisappear");
        Animator inGameAnimator = inGame.GetComponent<Animator>();
        inGameAnimator.SetBool("Next",true);
        string sad = "";
        foreach (AnimatorClipInfo anim in inGameAnimator.GetCurrentAnimatorClipInfo(0))
        {
            sad += $"{anim.clip.name}";
        }
        Debug.Log(sad);
        pause.SetActive(true);
        pause.GetComponent<Animator>().SetBool("Next",false);
        OnButtonClicked(true);
    }
    public void SetActiveButton()
    {
        gameObject.SetActive(false);
    }
    private bool IsAnimationPlaying(GameObject gameObject)
    {
/*        Debug.Log($"колво аним {gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash}");
        Debug.Log($"колво аним {gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length}");
        Debug.Log($"колво аним {gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).loop}");
        Debug.Log($"колво аним {gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime}");
        Debug.Log($"колво аним {gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash}");
        Debug.Log($"колво аним {gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).speed}");
        Debug.Log($"колво аним {gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).speedMultiplier}");
        Debug.Log($"колво аним {gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).tagHash}");
*/        Debug.Log($"колво аним {gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip}");
        return gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).loop;
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
        //StartCoroutine(TurnOff(pause, 0.75f));
        //pause.GetComponent<Animator>().SetTrigger("PauseDisappear");
        Animator inPauseAnimator= pause.GetComponent<Animator>();
        inPauseAnimator.SetBool("Next",true);
        string sad = "";
        inGame.SetActive(true);
        inGame.GetComponent<Animator>().SetBool("Next",false);
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

    private IEnumerator TurnOff(GameObject Who, float Time)
    {
        yield return new WaitForSeconds(Time);
        Who.SetActive(false);
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
