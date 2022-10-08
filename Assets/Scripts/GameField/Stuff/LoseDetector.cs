using System.Collections.Generic;
using UnityEngine;

class LoseDetector 
{
    public static bool IsLose(List<Vector2> coordinates, float endFieldYCoordinate)
    {
        foreach (float coordinate in ConvertListVector2ToArrayFloatYCoordinate(coordinates))
        {
            if (coordinate >= endFieldYCoordinate - 0.5)
                return true;
        }
        return false;
    }

    private static float[] ConvertListVector2ToArrayFloatYCoordinate(List<Vector2> coordinates)
    {
        float[] Ycoordinates = new float[4];
        for (int i = 0; i < coordinates.Count; i++)
        {
            Ycoordinates[i] = coordinates[i].y;
        }
        return Ycoordinates;
    }

}
