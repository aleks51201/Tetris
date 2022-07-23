using System.Collections;
using UnityEngine;

class Particle: MonoBehaviour
{
    [SerializeField]
    private GameObject particle;
    IEnumerator RemoveObject()
    {
        yield return new WaitForSeconds(3f);
        Destroy(particle);
    }
    private void Start()
    {
        StartCoroutine(RemoveObject());
    }
}
