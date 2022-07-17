/*using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.New.Outdate
{
    public class Figure : MonoBehaviour, IControlable, IGetFigureCellCoordinate
    {
        public GameObject tetromino;
        public LayerMask layerMask;

        private List<GameObject> rightNeighbours = new();
        private List<GameObject> leftNeighbours = new();

        private Dictionary<Type, IFigureBehaviour> behaviorsMap;
        private IFigureBehaviour behaviourCurrent;

        private void InitBehaviour()
        {
            this.behaviorsMap = new Dictionary<Type, IFigureBehaviour>();
            this.behaviorsMap[typeof(FigureBehaviourQueue)] = new FigureBehaviourQueue();
            this.behaviorsMap[typeof(FigureBehaviourGame)] = new FigureBehaviourGame();
            this.behaviorsMap[typeof(FigureBehaviourStash)] = new FigureBehaviourStash();
        }
        private void SetBehaviour(IFigureBehaviour newBehaviour)
        {
            if (this.behaviourCurrent != null)
                this.behaviourCurrent.Exit();
            this.behaviourCurrent = newBehaviour;
            this.behaviourCurrent.Enter();
        }
        private void SetBehaviourByDefault()
        {
            var behaviourByDefault = this.GetBehaviour<FigureBehaviourQueue>();
            SetBehaviour(behaviourByDefault);
        }
        private IFigureBehaviour GetBehaviour<T>() where T : IFigureBehaviour
        {
            var type = typeof(T);
            return this.behaviorsMap[type];
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
        public void Acceleration(bool toggle)
        {
            if (toggle)
                this.tetromino.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -6);
            else
                this.tetromino.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -3);
        }

        public void Rotate()
        {
            if (!IsRotateAllowed())
                this.tetromino.transform.Rotate(new Vector3(0f, 0f, 1), 90, Space.Self);
        }
        private bool IsRotateAllowed()
        {
            Collider2D ghost = GetChildGhost().GetComponent<Collider2D>();
            return ghost.IsTouchingLayers(layerMask);
        }

        private void ColorationCell()
        {
            foreach (Transform cell in GetAllChildCell())
            {
                cell.GetComponent<SpriteRenderer>().color = RandomColorFigureGame();
            }
        }
        private Color RandomColorFigureGame()
        {
            System.Random random = new();
            Color[] colorArray = ColorDataHolder.colorInGameFigure;
            return colorArray[random.Next(0, colorArray.Length)];
        }


        public void Dissolve()
        {
            DestroyGhost();
            this.tetromino.transform.DetachChildren();
            BusEvent.OnDeleteTetrominoEvent?.Invoke();
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
            Transform[] childObjects = GetAllChildObject();
            for (int i = 0; i < childObjects.Length; i++)
            {
                if (childObjects[i].CompareTag("Ghost"))
                {
                    return childObjects[i];
                }
            }
            //throw new NullReferenceException("Ghost not found");????
            return null;
        }

        public List<Transform> GetAllChildCell()
        {
            Transform[] childObject = GetAllChildObject();
            Transform childCell;
            List<Transform> childCells = new();
            for (int i = 0; i < childObject.Length; i++)
            {
                childCell = childObject[i];
                if (childCell.CompareTag("Figure"))
                {
                    childCells.Add(childCell);
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
        private void Start()
        {
            InitBehaviour();
            SetBehaviourByDefault();
            ColorationCell();
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
}
*/

/*    public delegate void CreateHandler(GameObject newTetromino);
    public event CreateHandler OnCreateTetrominoEvent;*/