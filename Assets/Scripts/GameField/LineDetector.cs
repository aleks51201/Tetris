using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class LineDetector
{
    private int heightField=0;
    private int widthField = 0;
    private int numObjectOnLine;
    private LayerMask maskName;

    public LineDetector(int heightField,int widthField,LayerMask detectorMaskName,int objectOnLine)
    {
        this.heightField = heightField;
        this.widthField = widthField;
        this.maskName = detectorMaskName;
        this.numObjectOnLine = objectOnLine;
    }
    private RaycastHit2D[] GetDetectionObject(Vector3 detectorPosition)
    {
        return Physics2D.RaycastAll(detectorPosition, Vector2.right, this.widthField, maskName);
    }

   public int PatrolDetector(Vector2 detectorStartPosition)
    {
        Vector2 newPosition;
        int numLine = 0;
        for (int i = 0; i < this.heightField; i++)
        {
            newPosition = detectorStartPosition + new Vector2(0, i);
            if (GetDetectionObject(newPosition).Length == 0)
                break;
            RaycastHit2D[] detectedObject = GetDetectionObject(newPosition);
            StartDestroyObject(detectedObject);
            if (isLineFull(detectedObject))
                numLine++;
        }
        return numLine;
    }
    private bool isLineFull(RaycastHit2D[] detectedObject)
    {
        return (detectedObject.Length == numObjectOnLine);
    }
    private void StartDestroyObject(RaycastHit2D[] detectedObject)
    {
        if (isLineFull(detectedObject))
            BusEvent.OnLineIsFullEvent?.Invoke(detectedObject);
    }
}
