using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Scripts.Gameplay.EightTrigrams
{
    public class TrigramTrigger : MonoBehaviour
    {
        public TrigramRing targetRing;
        private void OnTriggerEnter(Collider other)
        {
            TrigramRotateSubject.Instance.Notify(targetRing);
        }
        void Disappear(object arg)
        {
            foreach (var c in GetComponents<Collider>())
                c.enabled = false;
            transform.DOMoveY(transform.position.y - 0.2f, 3f);
        }
        private void OnEnable()
        {
            TrigramUnlockSubject.Instance.Register(Disappear);
        }
        private void OnDisable()
        {
            TrigramUnlockSubject.Instance.Unregister(Disappear);
        }
    }
}