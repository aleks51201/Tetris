using UnityEngine;

class LockStash : MonoBehaviour
{
    private Animator lockAnimator;
    private bool deleted=true;

    private void Start()
    {
        lockAnimator = this.gameObject.GetComponent<Animator>();
    }

    private void TriggerLockAnimation(bool isLock)
    {
        if (deleted)
            return;
        if (isLock)
        {
            lockAnimator.SetTrigger("FullLock");
            return;
        }
        lockAnimator.SetTrigger("FullUnlock");
    }

    private void OnDeleteTetromino(GameObject _)
    {
        TriggerLockAnimation(false);
        deleted = true;
    }

    private void OnSwitchTetrimino()
    {
        deleted = false;
        TriggerLockAnimation(true);
    }

    private void OnEnable()
    {
        BusEvent.OnSwitchTetrominoEvent += OnSwitchTetrimino;
        BusEvent.OnDeleteTetrominoEvent += OnDeleteTetromino;
    }

    private void OnDisable()
    {
        BusEvent.OnSwitchTetrominoEvent -= OnSwitchTetrimino;
        BusEvent.OnDeleteTetrominoEvent -= OnDeleteTetromino;
    }
}
