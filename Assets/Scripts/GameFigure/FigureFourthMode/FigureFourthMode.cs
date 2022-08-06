using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class FigureFourthMode: FigureThirdMode
{
    public override void ColorationCell()
    {
        Color hue;
        foreach (Transform cell in GetAllChildObject())
        {
            hue = RandomColorFigureGame();
            cell.GetComponent<SpriteRenderer>().color = hue;
        }
    }
    private protected override void EndContolTetromino()
    {
        field.AddMatrixTetromino(GetAllChildObject());
        field.MatrixShift();
        field.detectedObjects = field.LineDetector();
        //field.PrintMatrixField();
        if (field.IsFullDetectedList(field.detectedObjects))
        {
            field.StartDestroyAnimation(field.detectedObjects);
            field.RemoveMatrixTetromino(field.detectedObjects);
            field.StartAfterDestroyAnimation();
        }
        BusEvent.OnCollisionEnterEvent?.Invoke();
    }

}
