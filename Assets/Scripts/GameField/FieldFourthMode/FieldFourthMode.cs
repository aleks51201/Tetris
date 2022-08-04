using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class FieldFourthMode : FieldThirdMode
{
    List<Vector2> savedCoordinate;
    private protected override void MatrixShift()
    {
        foreach(Vector2 coordinate in this.savedCoordinate)
        {
            int x;
            for (int y = (int)coordinate.y + 1; y < this.fieldHeight; y++)
            {
                x = (int)coordinate.x;
                Transform cell = this.matrixField[y][x];
                this.matrixField[y - 1][x] = cell;
                this.matrixField[y][x] = null;
                if(cell!=null)
                    cell.position = new Vector2(x, y - 1);
            }
        }
    }

    private void SaveCoordinate(List<Transform> detectedObjects)
    {
        savedCoordinate = new();
        if (detectedObjects.Count < 2)
            return;
        foreach(Transform cell in detectedObjects)
        {
            savedCoordinate.Add(cell.position);
        }
    }
    private List<Transform> FindChainOnHorizontal(Transform cell, Color hue)
    {
        List<Transform> detectedObjectsOnHorizontal = new();
        Vector2[] directions = { new Vector2(1, 0), new Vector2(-1, 0) };
        foreach (Vector2 direct in directions)
        {
            detectedObjectsOnHorizontal.AddRange(ChainWalk(cell.position, direct, hue));
        }
        detectedObjectsOnHorizontal.Add(cell);
        if (detectedObjectsOnHorizontal.Count >= 3)
            return detectedObjectsOnHorizontal;
        return null;
    }
    private List<Transform> FindChainOnVertical(Transform cell, Color hue)
    {
        List<Transform> detectedObjectsOnVertical = new();
        Vector2[] directions = { new Vector2(0, 1), new Vector2(0, -1), };
        foreach (Vector2 direct in directions)
        {
            detectedObjectsOnVertical.AddRange(ChainWalk(cell.position, direct, hue));
        }
        detectedObjectsOnVertical.Add(cell);
        if (detectedObjectsOnVertical.Count >= 3)
            return detectedObjectsOnVertical;
        return null;
    }
    private IEnumerable<Transform> FindChainOnDirections(Transform cell, Color hue)
    {
        List<Transform> detectedObjects = new();
        List<Transform> verticalChain = FindChainOnVertical(cell, hue);
        List<Transform> horizontalChain = FindChainOnHorizontal(cell, hue);
        if (verticalChain != null)
            detectedObjects.AddRange(verticalChain);
        if (horizontalChain != null)
            detectedObjects.AddRange(horizontalChain);
        List<Transform> distinctDetectedObjects = detectedObjects.Distinct().ToList();
        return distinctDetectedObjects;
    }
    public override List<Transform> LineDetector()
    {
        Transform[] childObjects = GetChildObject;
        Color hue;
        List<Transform> detectedObjects = new();
        for (int i = 0; i < childObjects.Length; i++)
        {
            Transform cell = childObjects[i];
            hue = cell.GetComponent<SpriteRenderer>().color;
            detectedObjects.AddRange(FindChainOnDirections(cell, hue));
        }
        List<Transform> distinctDetectedObjects = detectedObjects.Distinct().ToList();
        SaveCoordinate(distinctDetectedObjects);
        return distinctDetectedObjects;
    }

    private bool IsOutOfRange(int x, int y)
    {
        return !(0 <= x && x < this.FieldWidth && 0 <= y && y < this.FieldHeigh);
    }
    private bool IsCell(int x, int y)
    {
        return this.matrixField[y][x] != null;
    }
    private List<Transform> ChainWalk(Vector2 startPosition, Vector2 direction, Color color)
    {
        List<Transform> detectedObjects = new();
        Color currentColor;
        int x = (int)startPosition.x;
        int y = (int)startPosition.y;
        while (true)
        {
            x += (int)direction.x;
            y += (int)direction.y;
            if (IsOutOfRange(x, y) || !IsCell(x, y))
                return detectedObjects;
            Transform cell = this.matrixField[y][x];
            currentColor = cell.GetComponent<SpriteRenderer>().color;
            if (currentColor != color)
                return detectedObjects;
            if (!detectedObjects.Exists(isInside => isInside == cell))
                detectedObjects.Add(cell);
        }
    }
    public override bool IsFullDetectedList(List<Transform> detectedObjects)
    {
        return detectedObjects.Count >= 3 ;
    }
}
