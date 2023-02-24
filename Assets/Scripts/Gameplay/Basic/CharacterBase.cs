using System.Collections;
using UnityEngine;
using Scripts.Gameplay.BuffSystem;
using System.Collections.Generic;

namespace Scripts.Gameplay.Basic
{
    public abstract class CharacterBase : MonoBehaviour
    {
        [Header("属性")]
        public int maxHP;
        protected int _currentHP;
        public int currentHP { get => _currentHP; }

        protected List<Buff> buffs;

        protected virtual void Init()
        {
            _currentHP = maxHP;
            buffs = new List<Buff>();
        }
        protected abstract void InitBuffCallback(Buff buff);
        public void StartBuff(Buff buff)
        {
            InitBuffCallback(buff);
            buffs.Add(buff);
            buff.Start();
        }
        public void InterruptBuff(Buff buff)
        {
            buff.Interrupt();
        }
        public void EndBuff(Buff buff)
        {
            buffs.Remove(buff);
            buff.Finish();
        }
    }
}