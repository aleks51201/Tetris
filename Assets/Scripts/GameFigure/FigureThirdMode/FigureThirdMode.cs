using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class FigureThirdMode : FigureBase
{
    public override void Move(KeyCode keyCode, float direct)
    {
        throw new NotImplementedException();
    }

    public override void Rotate(KeyCode keyCode, float direct)
    {
        throw new NotImplementedException();
    }
    public override void Acceleration(KeyCode keyCode, float direct)
    {
        throw new NotImplementedException();
    }

    public override Vector2 GetCurrentPosition()
    {
        throw new NotImplementedException();
    }

    public override void ColorationCell()
    {
        throw new NotImplementedException();
    }

    private protected override Color RandomColorFigureGame()
    {
        throw new NotImplementedException();
    }
    
    public override List<Vector2> GetChildCoordinate()
    {
        List<Vector2> coordinates = new();
        foreach (Transform child in GetAllChildObject())
        {
            coordinates.Add(child.position);
        }
        return coordinates;
    }

    private protected override void Dissolve()
    {
        throw new NotImplementedException();
    }
}
