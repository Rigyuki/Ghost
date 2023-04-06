using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Gameplay.Basic;
using Scripts.Gameplay.BuffSystem;
using Spine.Unity;

namespace Scripts.Gameplay.Trap
{
    public class VineTrap : TrapBase
    {
        [SerializeField] float freezeTime = 2f;
        [SerializeField] string waitAnimation = "di2";
        [SerializeField] string riseAnimation = "shou1";
        [SerializeField] string stayAnimation = "shou2";
        [SerializeField] SkeletonAnimation sa;
        bool charged = true;
        protected override void TakeEffect(PlayerController player)
        {
            if (!charged) return;
            player.StartBuff(new Buff(this, BuffType.Frozen, lastTime: freezeTime));
            StartCoroutine(AfterTrigger());
        }
        IEnumerator AfterTrigger()
        {
            charged = false;
            sa.gameObject.SetActive(true);
            sa.state.SetAnimation(0, riseAnimation, false);
            sa.state.AddAnimation(0, stayAnimation, true, 0);
            yield return new WaitForSeconds(freezeTime);
            //Debug.Log("!");
            var backAnim = sa.state.SetAnimation(0, riseAnimation, false);
            backAnim.TrackTime = backAnim.AnimationEnd;
            backAnim.TimeScale = -1;
            while (backAnim.AnimationTime > 0.01f)
                yield return null;
            sa.gameObject.SetActive(false);
            sa.state.SetAnimation(0, waitAnimation, true);
            charged = true;
            yield break;
        }
    }
}