using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Gameplay.Basic;

namespace Scripts.Gameplay.Trap
{
    [RequireComponent(typeof(Collider))]
    public abstract class TrapBase : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                TakeEffect(other.GetComponent<PlayerController>());
            }
        }
        protected abstract void TakeEffect(PlayerController player);
    }
}