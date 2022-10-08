using System.Collections;
using UnityEngine;

internal class CellDestroyHandler : MonoBehaviour
{
    private bool isDestroyed = false;

    private void ChangeDestroyStatus()
    {
        isDestroyed = true;
    }

    private IEnumerator MainLoop()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (isDestroyed)
                BusEvent.OnStartAfterDestoyAnimation?.Invoke();
            isDestroyed = false;
        }
    }

    private void Start()
    {
        StartCoroutine(MainLoop());
        
    }

    private void OnEnable()
    {
        BusEvent.OnCellDestroyEvent += ChangeDestroyStatus;
    }

    private void OnDisable()
    {
        BusEvent.OnCellDestroyEvent -= ChangeDestroyStatus;
        
    }
}
