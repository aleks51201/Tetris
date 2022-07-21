using System.Collections;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public GameObject welcomePanel, inGame, pause, lose, startGame, gameField;
    ICreatable createFigure;
    IPauseable pauseGane;

    public void OnPlayButtonPressed() //�������� ������� ���� ����� ��������� �� �����
    {
        StartCoroutine(TurnOff(startGame, 0.55f));
        startGame.GetComponent<Animator>().SetTrigger("StartDisappear");
        inGame.SetActive(true);
        if (createFigure != null)
            createFigure.Create();
    }
    public void OnPauseButtonPressed() //���������������� ������� �������� �� ������������� �� ������ ����������
    {
        StartCoroutine(TurnOff(inGame, 0.75f));
        inGame.GetComponent<Animator>().SetTrigger("InGameDisappear");
        pause.SetActive(true);
        OnButtonClicked(true);
    }
    public void OnRestartButtonPressed() //���������� �����
    {
        SceneTransition.SwitchScene("PhisicOne");
        if (pauseGane != null)
            pauseGane.ContinueGame();
    }
    public void OnMenuButtonPressed() //�������� ����� ����
    {
        SceneTransition.SwitchScene("Menu");  //������ ����� �������� 0.75f �������
        if (pauseGane != null)
            pauseGane.ContinueGame();
    }
    public void OnContinueButtonPressed() // ������������� �������� ���� 
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
    }
    private void OnEnable()
    {
        BusEvent.OnLoseGameEvent += OnLose;
    }
    private void OnDisable()
    {
        BusEvent.OnLoseGameEvent -= OnLose;
    }
}
