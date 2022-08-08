using System;
using UnityEngine;

public static class BusEvent
{
    public static Action<GameObject> OnDeleteTetrominoEvent;
    public static Action<GameObject> OnCreateTetrominoEvent;
    public static Action OnAddObjectToQueueEvent;
    public static Action OnQueueFullEvent;
    public static Action<GameObject> OnSpawnTetrominoEvent;
    public static Action OnLoseGameEvent;
    public static Action OnSwitchTetrominoEvent;
    public static Action<int> OnNewPointsEvent;
    public static Action<Vector2> OnStartAccelerationEvent;
    public static Action<Vector2> OnEndAccelerationEvent;
    public static Action<bool> OnPauseEvent;
    public static Action<RaycastHit2D[]> OnLineIsFullEvent;
    public static Action<int> OnAddScoreEvent;
    public static Action OnCollisionEnterEvent;
    public static Action OnCellDestroyEvent;
    public static Action OnStartDestroyAnimationEvent;

    //input system
    public static Action<KeyCode,float> OnKeyDownEvent;
    public static Action<KeyCode,float> OnKeyUpEvent;
    public static Action<KeyCode, float> OnKeyHoldEvent;
}

