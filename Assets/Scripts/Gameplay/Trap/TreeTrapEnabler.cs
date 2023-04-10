using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace Scripts.Gameplay.Trap
{
    public class TreeTrapEnabler : MonoBehaviour
    {
        [SerializeField] string stretchAnimation = "1";
        [SerializeField] SkeletonAnimation sa;
        [SerializeField] Collider treeTrap;
        bool charged = true;
        private void OnTriggerEnter(Collider other)
        {
            if (!charged || other.gameObject.layer != LayerMask.NameToLayer("Player"))
                return;
            StartCoroutine(AfterTrigger());
        }
        IEnumerator AfterTrigger()
        {
            charged = false;
            var anim = sa.state.SetAnimation(0, stretchAnimation, false);
            sa.timeScale = 1;
            sa.state.TimeScale = 1;
            while (anim.AnimationTime < 0.8f)
                yield return null;
            treeTrap.enabled = true;
            while (!anim.IsComplete)
                yield return null;
            anim = sa.state.SetAnimation(0, stretchAnimation, false);
            anim.TrackTime = anim.AnimationEnd;
            sa.state.TimeScale = -1;
            while (anim.AnimationTime > 0.8f)
                yield return null;
            treeTrap.enabled = false;
            while (anim.AnimationTime > 0.1f)
                yield return null;
            sa.timeScale = 0;
            charged = true;
        }
    }
}