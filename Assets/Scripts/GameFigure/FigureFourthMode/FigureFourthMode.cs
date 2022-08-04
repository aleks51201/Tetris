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

}
