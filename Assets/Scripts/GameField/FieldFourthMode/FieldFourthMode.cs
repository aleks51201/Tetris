using System.Collections.Generic;
using UnityEngine;

internal class FieldFourthMode : FieldThirdMode
{
    private void MatrixShift(Vector2 deletedObject)
    {
        int x;
        for (int y = (int)deletedObject.y + 1; y < this.fieldHeight; y++)
        {
            x = (int)deletedObject.x;
            Transform cell = this.matrixField[y][x];
            this.matrixField[y - 1][x] = cell;
            cell.position = new Vector2(x, y - 1);
        }
    }

    private void LineDetector(Transform[] childObjects)
    {
        Color hue = new();
        foreach (Transform cell in childObjects)
        {
            hue = cell.GetComponent<SpriteRenderer>().color;


        }
    }

    private List<Transform> ChainWalk(Vector2 startPosition, Vector2 direction, Color color)
    {
        Color currentColor = new();
        List<Transform> detectedObjects = new();
        int x = (int)startPosition.x;
        int y = (int)startPosition.y;
        while (true)
        {
            x += (int)direction.x;
            y += (int)direction.y;
            currentColor = this.matrixField[y][x].GetComponent<SpriteRenderer>().color;
            if (currentColor != color)
                return detectedObjects;
            detectedObjects.Add(this.matrixField[y][x]);
        }
    }
    private void CheckChainOnColumn(Vector2 startPosition, Vector2 direction, Color color)
    {

    }
}
