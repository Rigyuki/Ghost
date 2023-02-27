using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Gameplay.Basic;
using Scripts.Gameplay.BuffSystem;

namespace Scripts.Gameplay.Trap
{
    public class VineTrap : TrapBase
    {
        public float freezeTime = 2f;
        protected override void TakeEffect(PlayerController player)
        {
            player.StartBuff(new Buff(this, BuffType.Frozen, lastTime: freezeTime));
            Destroy(gameObject);
        }
    }
}