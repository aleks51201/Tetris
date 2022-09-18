using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.UI
{
    class CloseAnimation: MonoBehaviour
    {
        public void SetActive()
        {
            this.gameObject.SetActive(false);
        }
    }
}
