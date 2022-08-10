using UnityEngine;
using UnityEngine.EventSystems;

internal class Mode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject preview;

    private Animator previewAnimator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        previewAnimator.SetTrigger("Start");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        previewAnimator.SetTrigger("Stop");
    }
    private void Start()
    {
        previewAnimator = preview.GetComponent<Animator>();
    }
}
