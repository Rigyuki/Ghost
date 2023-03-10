using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Door
{
    public class DoorEnabler : MonoBehaviour
    {
        [SerializeField] DoorOpen door;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
                door.enabled = true;
        }
    }
}