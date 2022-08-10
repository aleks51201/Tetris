using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

class Mode:MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
