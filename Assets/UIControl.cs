using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public GameObject WelcomePanel,InGame,Pause,Lose,StartGame;
    public void OnPlayButtonPressed() //�������� ������� ���� ����� ��������� �� �����
    {
        StartGame.SetActive(false);
        InGame.SetActive(true);
    }
    public void OnPauseButtonPressed() //���������������� ������� �������� �� ������������� �� ������ ����������
    {
        InGame.SetActive(false);
        Pause.SetActive(true);
    }
    public void OnRestartButtonPressed() //���������� �����
    {

    }
    public void OnMenuButtonPressed() //�������� ����� ����
    {

    }
    public void OnContinueButtonPressed() // ������������� �������� ���� 
    {
        Pause.SetActive(false);
        InGame.SetActive(true);
    }
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(1);
    }
}
