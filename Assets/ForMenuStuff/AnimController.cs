using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("StartAnim");
    }
    public IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(16);
        gameObject.GetComponent<Animator>().SetTrigger("StartPlaying");
    }
}
