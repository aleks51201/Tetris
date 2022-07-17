using UnityEngine;
public interface IControlable
{
    public void Move(float direct);
    public void Acceleration(sbyte toggle);
    public void Rotate(bool toggle);
}
