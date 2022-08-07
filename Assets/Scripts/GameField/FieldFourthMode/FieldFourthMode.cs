using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class FieldFourthMode : FieldThirdMode
{
    private List<Vector2> savedCoordinate;

    public override void MatrixShift()
    {
        List<Transform> detectedObjects = new();
        List<List<Transform>> newMatrix = CreateMatrix(this.fieldWidth, this.FieldHeigh);
        for (int x = 0; x < this.fieldWidth; x++)
        {
            int rowIndex = 0;
            for (int y = 0; y < this.FieldHeigh; y++)
            {
                Transform cell = this.matrixField[y][x];
                if (cell == null)
                    continue;
                cell.position = new Vector2(x, rowIndex);
                newMatrix[rowIndex][x] = cell;
                rowIndex++;
            }
        }
        this.matrixField = newMatrix;
        detectedObjects=LineDetector(this.matrixField);
        if (IsFullDetectedList(detectedObjects))
        {
            StartDestroyAnimation(detectedObjects);
            RemoveMatrixTetromino(detectedObjects);
            StartAfterDestroyAnimation();
        }

    }

    private void SaveCoordinate(List<Transform> detectedObjects)
    {
        savedCoordinate = new();
        if (detectedObjects.Count < 2)
            return;
        foreach (Transform cell in detectedObjects)
        {
            savedCoordinate.Add(cell.position);
        }
    }

    private List<Transform> FindChainOnHorizontal(Transform cell, Color hue)
    {
        List<Transform> detectedObjects = new();
        Vector2[] directions = { new Vector2(1, 0), new Vector2(-1, 0) };
        foreach (Vector2 direct in directions)
        {
            detectedObjects.AddRange(ChainWalk(cell.position, direct, hue));
        }
        detectedObjects.Add(cell);
        if (detectedObjects.Count < 3)
            return new List<Transform>();
        return detectedObjects;
    }

    private List<Transform> FindChainOnVertical(Transform cell, Color hue)
    {
        List<Transform> detectedObjects = new();
        Vector2[] directions = { new Vector2(0, 1), new Vector2(0, -1), };
        foreach (Vector2 direct in directions)
        {
            detectedObjects.AddRange(ChainWalk(cell.position, direct, hue));
        }
        detectedObjects.Add(cell);
        if (detectedObjects.Count < 3)
            return new List<Transform>();
        return detectedObjects;
    }

    private List<Transform> FindChainOnRightDiagonal(Transform cell, Color hue)
    {
        List<Transform> detectedObjects = new();
        Vector2[] directions = { new Vector2(1, 1), new Vector2(-1, -1), };
        foreach (Vector2 direct in directions)
        {
            detectedObjects.AddRange(ChainWalk(cell.position, direct, hue));
        }
        detectedObjects.Add(cell);
        if (detectedObjects.Count < 3)
            return new List<Transform>();
        return detectedObjects;
    }

    private List<Transform> FindChainOnLeftDiagonal(Transform cell, Color hue)
    {
        List<Transform> detectedObjects = new();
        Vector2[] directions = { new Vector2(-1, 1), new Vector2(1, -1), };
        foreach (Vector2 direct in directions)
        {
            detectedObjects.AddRange(ChainWalk(cell.position, direct, hue));
        }
        detectedObjects.Add(cell);
        if (detectedObjects.Count < 3)
            return new List<Transform>();
        return detectedObjects;
    }

    private List<Transform> FindChainOnDirections(Transform cell, Color hue)
    {
        List<Transform> detectedObjects = new();
        List<Transform>[] chains =
        {
            FindChainOnVertical(cell, hue),
            FindChainOnHorizontal(cell, hue),
            FindChainOnRightDiagonal(cell, hue),
            FindChainOnLeftDiagonal(cell, hue),
        };
        foreach (List<Transform> chain in chains)
        {
            detectedObjects.AddRange(chain);
        }
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

    public List<Transform> LineDetector(List<List<Transform>> field)
    {
        Color hue;
        List<Transform> detectedObjects = new();
        foreach (List<Transform> row in field)
        {
            foreach (Transform cell in row)
            {
                if (cell == null)
                    continue;
                hue = cell.GetComponent<SpriteRenderer>().color;
                detectedObjects.AddRange(FindChainOnDirections(cell, hue));
            }
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
        return detectedObjects.Count >= 3;
    }
}
