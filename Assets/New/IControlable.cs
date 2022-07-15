using UnityEngine;
public interface IControlable
{
    public void Move(float direct);
    public void Acceleration(bool toggle);
    public void Rotate(bool toggle);
}
