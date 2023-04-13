using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Lantern
{
    [RequireComponent(typeof(Collider))]
    public class LanternReseter : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            LanternResetSubject.Instance.Notify(null);
        }
    }
}