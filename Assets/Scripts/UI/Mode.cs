using UnityEngine;
using UnityEngine.EventSystems;

internal class Mode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject preview;

    private Animator previewAnimator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        previewAnimator.SetBool("Active", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        previewAnimator.SetBool("Active", false);
    }

    private void Start()
    {
        previewAnimator = preview.GetComponent<Animator>();
    }
}
