using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace Scripts.Gameplay.Trap
{
    public class VineTrapOuter : MonoBehaviour
    {
        [SerializeField] SkeletonAnimation sa;
        [SerializeField] string appearAnim = "di1";
        [SerializeField] string waitAnim = "di2";
        bool awake;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
                return;
            if (awake)
                return;
            StartCoroutine(Appear());
        }
        IEnumerator Appear()
        {
            sa.timeScale = 1;
            awake = true;
            var anim = sa.state.SetAnimation(0, appearAnim, false);
            while (!anim.IsComplete)
                yield return null;
            sa.state.SetAnimation(0, waitAnim, false);
        }
    }
}