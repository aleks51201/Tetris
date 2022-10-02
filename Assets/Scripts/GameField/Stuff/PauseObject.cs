using UnityEngine;

class PauseObject
{
    public static void Pause(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
    public static void UnPause(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
    public static void Pause(Transform gameObject)
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        RigidbodyType2D rbtype = rb.bodyType;
        if (rbtype == RigidbodyType2D.Dynamic)
        {
             rb.bodyType = RigidbodyType2D.Static;
        }
    }
    public static void UnPause(Transform gameObject)
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        RigidbodyType2D rbtype = rb.bodyType;
        if (rbtype == RigidbodyType2D.Static)
        {
             rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
