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
}
