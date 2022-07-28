using UnityEngine;


public class CellThirdMode : MonoBehaviour
{
    public GameObject childCell;

    public void DestoyCell()
    {
        Destroy(this.childCell);
        BusEvent.OnCellDestroyEvent?.Invoke();
    }
}
