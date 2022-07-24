using UnityEngine;
public interface IControlable
{
    public void Move(KeyCode keyCode,float direct);
    public void Acceleration(KeyCode keyCode,float direct);
    public void Rotate(KeyCode keyCode,float direct);
}
