using System;
using UnityEngine;

public static class BusEvent
{
    public static Action<GameObject> OnDeleteTetrominoEvent;
    public static Action<GameObject> OnCreateTetrominoEvent;
    public static Action OnAddObjectToQueueEvent;
    public static Action OnQueueFullEvent;
    public static Action<GameObject> OnSpawnTetrominoEvent;
}

