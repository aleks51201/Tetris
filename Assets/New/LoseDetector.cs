using UnityEngine;

class LoseDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Figure"))
        {
            BusEvent.OnLoseGameEvent?.Invoke();
        }
    }
}
