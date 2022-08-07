using UnityEngine;


public class CellThirdMode : MonoBehaviour
{
    private GameObject childCell;

    public void DestoyCell()
    {
        Destroy(this.childCell);
        BusEvent.OnCellDestroyEvent?.Invoke();
    }

    private void Start()
    {
        childCell = this.gameObject;
    }
}
