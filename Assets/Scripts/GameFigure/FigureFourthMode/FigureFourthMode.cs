using UnityEngine;

class FigureFourthMode : FigureThirdMode
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
        if (field.IsFullDetectedList(field.detectedObjects))
        {
            field.StartDestroyAnimation(field.detectedObjects);
            field.RemoveMatrixTetromino(field.detectedObjects);
            field.StartAfterDestroyAnimation(field.detectedObjects, field.GameScore);
        }
        BusEvent.OnCollisionEnterEvent?.Invoke();
    }
}
