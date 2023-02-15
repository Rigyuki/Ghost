using System.Collections;
using UnityEngine;

namespace Scripts.Gameplay.EightTrigrams
{
    public class TrigramTrigger : MonoBehaviour
    {
        public TrigramRing targetRing;
        private void OnTriggerEnter(Collider other)
        {
            TrigramRotateSubject.Instance.Notify(targetRing);
        }
    }
}