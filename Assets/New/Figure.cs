using System;
using System.Collections.Generic;
using UnityEngine;


public class Figure : MonoBehaviour, IControlable, IGetFigureCellCoordinate
{
    public GameObject tetromino;

    public List<GameObject> rightNeighbours = new();
    public List<GameObject> leftNeighbours = new();

    public void Remove()
    {

    }

    public void Move(float direct)
    {
        if (IsNeighborsEmpty(direct))
        {
            Vector2 positionOffset = new Vector2(direct, 0);
            Vector2 newPosition = GetCurrentPosition() + positionOffset;

            this.tetromino.transform.position = newPosition;
        }

    }

    public void Rotate()
    {
        this.tetromino.transform.Rotate(new Vector3(0f, 0f, 1), 90, Space.Self);
    }

    public delegate void DeleteHandler();
    public event DeleteHandler OnDeleteTetrominoEvent;
    public void Dissolve()
    {
        DestroyGhost();
        this.tetromino.transform.DetachChildren();
        OnDeleteTetrominoEvent?.Invoke();
        Destroy(tetromino);


    }
    private void DestroyGhost()
    {
        Destroy(GetChildGhost().gameObject);
    }
    public Vector2 GetCurrentPosition()
    {
        return this.tetromino.transform.position;
    }
    public Transform[] GetAllChildObject()
    {
        return this.tetromino.GetComponentsInChildren<Transform>()[1..^0];
    }
    public Transform GetChildGhost()
    {
        Transform[] childObject = GetAllChildObject();
        Transform Ghost;
        for (int i = 0; i < childObject.Length; i++)
        {
            if (childObject[i].tag == "Ghost")
            {
                return childObject[i];
            }
        }
        throw new NullReferenceException("Ghost not found");
    }

    public Transform[] GetAllChildCell()
    {
        Transform[] childObject = GetAllChildObject();
        Transform childCell;
        Transform[] childCells = new Transform[4];
        for (int i = 0; i < childObject.Length; i++)
        {
            childCell = childObject[i];
            if (childCell.tag == "figure")
            {
                childCells[i] = childCell;
            }
        }
        return childCells;
    }

    public List<Vector3> GetCoodinate()
    {
        List<Vector3> coordinates = new();
        foreach (Transform child in GetAllChildCell())//?????
        {
            coordinates.Add(child.position);
        }
        return coordinates;
    }
    private void SetNeighborsCoordinates(Collider2D collider)
    {
        Vector3 unallocatedCoordinate = collider.transform.position;
        Vector3 rightNeighbor = unallocatedCoordinate;
        Vector3 leftNeighbor = unallocatedCoordinate;

        foreach (Vector3 childCoordinate in GetCoodinate())
        {
            if (childCoordinate.x <= unallocatedCoordinate.x)
            {
                if (rightNeighbours.Exists(i => i == collider.gameObject))
                    continue;
                rightNeighbours.Add(collider.gameObject);
            }
            else if (childCoordinate.x >= unallocatedCoordinate.x && leftNeighbor != childCoordinate)
            {
                if (leftNeighbours.Exists(i => i == collider.gameObject))
                    continue;
                leftNeighbours.Add(collider.gameObject);
            }
        }
    }
    private void RemoveNeighborsCoordinates(Collider2D collider)
    {
        rightNeighbours.RemoveAll(i => i == collider.gameObject);
        leftNeighbours.RemoveAll(i => i == collider.gameObject);
    }
    private bool IsNeighborsEmpty(float direction)
    {
        switch (direction)
        {
            case 1:
                return rightNeighbours.Count == 0;
            case -1:
                return leftNeighbours.Count == 0;
            default:
                throw new NullReferenceException("IsNeighborsEmpty: don't have any direction");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetNeighborsCoordinates(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveNeighborsCoordinates(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (this.tetromino.GetComponent<Rigidbody2D>().velocity.y > -0.5f)
            Dissolve();
    }
}