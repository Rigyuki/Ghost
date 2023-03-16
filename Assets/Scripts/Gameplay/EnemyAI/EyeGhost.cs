using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.EnemyAI
{
    public class EyeGhost : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
                return;
            Debug.Log("Hit");
        }
    }
}