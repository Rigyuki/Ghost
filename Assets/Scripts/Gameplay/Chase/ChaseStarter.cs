using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Chase
{
    [RequireComponent(typeof(BoxCollider))]
    public class ChaseStarter : MonoBehaviour
    {
        public int segment;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
            {
                ChaseStartSubject.Instance.Notify(segment);
                Destroy(gameObject);
            }
        }
    }
}