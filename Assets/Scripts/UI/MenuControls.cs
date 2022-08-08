using System.Collections;
using UnityEngine;
public class MenuControls : MonoBehaviour
{
    public GameObject Settings;
    public GameObject Play;
    public GameObject Loading;

    public void OnPlayButtonClick()
    {
        Play.SetActive(true);
    }

    public void OnSettingsButtonClick()
    {
        Settings.SetActive(true);
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OnCrossButtonClick()
    {
        Settings.GetComponent<Animator>().SetTrigger("CrossPressed");
        StartCoroutine("SetUnActive");
    }

    public void OnLinkButtonClick()
    {
        Application.OpenURL("https://github.com/aleks51201/Tetris");
    }

    public void OnPlayBackButtonClick()
    {
        Play.GetComponent<Animator>().SetTrigger("Back");
        StartCoroutine("SetUnActive2");
    }

    public void OnModebuttonClick()
    {
        Loading.SetActive(true);
    }

    public void OnModeChangeFirstClick()
    {
        SceneTransition.SwitchScene("PhisicOne");
    }

    public void OnModeChangeSecondClick()
    {
        SceneTransition.SwitchScene("PhisicTwo");
    }

    public void OnModeChangeThirdClick()
    {
        SceneTransition.SwitchScene("MatrixOne");
    }
    public void OnModeChangeFourthClick()
    {
        SceneTransition.SwitchScene("MatrixTwo");
    }

    private IEnumerator SetUnActive()
    {
        yield return new WaitForSeconds(1f);
        Settings.SetActive(false);
    }

    private IEnumerator SetUnActive2()
    {
        yield return new WaitForSeconds(1f);
        Play.SetActive(false);
    }
}
