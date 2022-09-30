using System.Linq;
using UnityEngine;


class LineDetector
{
    private int heightField = 0;
    private int widthField = 0;
    private int numObjectOnLine;
    private LayerMask maskName;
    private RaycastHit2D[] allObjects;


    public LineDetector(int heightField, int widthField, LayerMask detectorMaskName, int objectOnLine)
    {
        this.heightField = heightField;
        this.widthField = widthField;
        maskName = detectorMaskName;
        numObjectOnLine = objectOnLine;
    }

    private RaycastHit2D[] GetDetectionObject(Vector3 detectorPosition)
    {
        return Physics2D.RaycastAll(detectorPosition, Vector2.right, widthField, maskName);
    }

    private RaycastHit2D[] ConcatenationArrays(RaycastHit2D[] firstArray, RaycastHit2D[] secondArray)
    {
        RaycastHit2D[] newArray = firstArray.Concat(secondArray).ToArray();
        return newArray;
    }

    public void LinePatrol(Vector2 detectorStartPosition)
    {
        Vector2 newPosition;
        RaycastHit2D[] allDetectedObjects = new RaycastHit2D[0];
        for (int i = 0; i < heightField; i++)
        {
            newPosition = detectorStartPosition + new Vector2(0, i);
            RaycastHit2D[] detectedObjects = GetDetectionObject(newPosition);
            if (detectedObjects.Length == 0)
                break;
            if (isLineFull(detectedObjects))
            {
                allDetectedObjects = ConcatenationArrays(allDetectedObjects, detectedObjects);
            }
        }
        if (allDetectedObjects.Length != 0 && !AreArraysEqual(allDetectedObjects))
        {
            allObjects = allDetectedObjects;
            StartDestroyObject(allObjects);
        }
    }

    private bool AreArraysEqual(RaycastHit2D[] newArray)
    {
        if (allObjects == null)
            return false;
        if (allObjects.Length != newArray.Length)
            return false;
        for (int i = 0; i < newArray.Length; i++)
        {
            if (allObjects[i] != newArray[i])
                return false;
        }
        return true;
    }

    private bool isLineFull(RaycastHit2D[] detectedObject)
    {
        return detectedObject.Length == numObjectOnLine;
    }

    private void StartDestroyObject(RaycastHit2D[] detectedObject)
    {
        BusEvent.OnLineIsFullEvent?.Invoke(detectedObject);
    }
}
