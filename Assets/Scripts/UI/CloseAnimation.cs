using UnityEngine;


namespace Assets.Scripts.UI
{
    internal class CloseAnimation : MonoBehaviour
    {
        public void SetActive()
        {
            this.gameObject.SetActive(false);
        }
    }
}
