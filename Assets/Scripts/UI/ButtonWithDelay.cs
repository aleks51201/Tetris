using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
internal class ButtonWithDelay : MonoBehaviour, IPointerClickHandler
{
    [FormerlySerializedAs("onClickWithDelay")]
    [SerializeField]
    private UnityEvent onClick = new();

    private bool isPressed;

    private void Press()
    {
        if (isPressed)
            return;
        onClick.Invoke();
        StartCoroutine(ClickTimeout());
    }

    private IEnumerator ClickTimeout()
    {
        isPressed = true;
        yield return new WaitForSeconds(0.5f);
        isPressed = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Press();
    }
}
