using System.Collections.Generic;
using UnityEngine;

public abstract class FigureBase : MonoBehaviour, IControlable
{
    [SerializeField]
    public GameObject tetromino;
    [SerializeField]
    private protected GameObject particle;

    public abstract void Move(KeyCode keyCode, float direct);
    public abstract void Acceleration(KeyCode keyCode, float direct);
    public abstract void Rotate(KeyCode keyCode, float direct);
    public abstract void ColorationCell();
    private protected abstract void Dissolve();
    public abstract Vector2 GetCurrentPosition();
    public abstract List<Vector2> GetChildCoordinate();
    private protected abstract Color RandomColorFigureGame();

    private protected virtual void ParticleStart()
    {
        Instantiate(particle, GetCurrentPosition(), Quaternion.identity);
    }

    public Transform[] GetAllChildObject()
    {
        return this.tetromino.GetComponentsInChildren<Transform>()[1..^0];
    }

}
