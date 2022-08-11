using UnityEngine;

class LineDetector
{
    private int heightField = 0;
    private int widthField = 0;
    private int numObjectOnLine;
    private LayerMask maskName;
    private int numLine = 0;

    enum Mode
    {
        Physics,
        MatrixOne,
        MatrixTwo,
    }

    public LineDetector(int heightField, int widthField, LayerMask detectorMaskName, int objectOnLine)
    {
        this.heightField = heightField;
        this.widthField = widthField;
        maskName = detectorMaskName;
        numObjectOnLine = objectOnLine;
    }
    private RaycastHit2D[] GetDetectionObject(Vector3 detectorPosition)
    {
        Debug.Log(Physics2D.RaycastAll(detectorPosition, Vector2.right, widthField, maskName));
        Debug.DrawRay(detectorPosition, Vector2.right, Color.red);
        return Physics2D.RaycastAll(detectorPosition, Vector2.right, widthField, maskName);
    }

    public void PatrolDetector(Vector2 detectorStartPosition)
    {
        Vector2 newPosition;
        numLine = 0;
        for (int i = 0; i < heightField; i++)
        {
            newPosition = detectorStartPosition + new Vector2(0, i);
            RaycastHit2D[] detectedObjects = GetDetectionObject(newPosition);
            if (detectedObjects.Length == 0)
                break;
            if (isLineFull(detectedObjects))
            {
                numLine++;
                StartDestroyObject(detectedObjects);
            }
        }
    }
    private bool isLineFull(RaycastHit2D[] detectedObject)
    {
        Debug.Log(detectedObject.Length);
        return detectedObject.Length == numObjectOnLine;
    }
    private void StartDestroyObject(RaycastHit2D[] detectedObject)
    {
        BusEvent.OnLineIsFullEvent?.Invoke(detectedObject);
    }
}
