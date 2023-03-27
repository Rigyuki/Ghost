using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Gameplay.Basic;

namespace Scripts.Gameplay.EnemyAI
{
    public class EyeGhost : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
                return;
            other.gameObject.GetComponent<CharacterBase>().TakeDamage(10);
            Debug.Log("Hit");
        }
    }
}