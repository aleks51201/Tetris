using System.Collections.Generic;
using UnityEngine;

class QueueField
{
    int queueLength = 0;
    public Queue<GameObject> queueOfTetromino;
    private float shift;


    public QueueField(int count,float shift)
    {
        this.queueLength = count;
        this.queueOfTetromino = new Queue<GameObject>(count+1);
        this.shift = shift;
    }

    public void AddObject(GameObject tetromino)
    {
        ShiftObjectsInQueue();
        queueOfTetromino.Enqueue(tetromino);
        CallNewFigure();
        QueueIsFull();
    }
    private void CallNewFigure()
    {
        if (queueOfTetromino.Count < queueLength)
            BusEvent.OnAddObjectToQueueEvent?.Invoke();
    }
    private void OnSpawnTetromino(GameObject fig)
    {
        CallNewFigure();
    }
    private void QueueIsFull()
    {
        if (queueOfTetromino.Count == queueLength)
            BusEvent.OnQueueFullEvent?.Invoke();
    }
    private void ShiftObjectsInQueue()
    {
        if (queueOfTetromino.Count != 0)
        {
            foreach (GameObject i in queueOfTetromino)
            {
                DisplacementFigure(new Vector2(0,3),i);
            }
        }
    }
    private void DisplacementFigure(Vector2 positionOffset, GameObject figure)
    { 
        Vector2 newPosition = GetCurrentPosition(figure) + positionOffset;
        figure.transform.position = newPosition;
    }
    private Vector2 GetCurrentPosition(GameObject figure)
    {
        return figure.transform.position;
    }

    
}

