using UnityEngine;


public class CellThirdMode : MonoBehaviour
{
    private GameObject childCell;

    public void DestoyCell()
    {
        Destroy(this.childCell);
        BusEvent.OnCellDestroyEvent?.Invoke();
    }
    private void OnPause(bool isPause)
    {
        Animator cellAnimator = gameObject.GetComponent<Animator>();
        if (isPause)
            cellAnimator.speed = 0;
        else
            cellAnimator.speed = 1;
    }

    private void Start()
    {
        childCell = this.gameObject;
    }

    private void OnEnable()
    {
        BusEvent.OnPauseEvent += OnPause;
    }

    private void OnDisable()
    {
        BusEvent.OnPauseEvent -= OnPause;
    }
}
