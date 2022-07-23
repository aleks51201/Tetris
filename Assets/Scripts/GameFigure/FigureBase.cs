using UnityEngine;

public abstract class FigureBase: MonoBehaviour,IControlable
{
    public abstract void Move(float direct);
    public abstract void Acceleration(sbyte toggle);
    public abstract void Rotate(bool toggle);
}
