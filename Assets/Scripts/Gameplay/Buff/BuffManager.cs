using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.CustomTool.DesignPatterns;
using System;
using Scripts.Gameplay.Basic;

namespace Scripts.Gameplay.BuffSystem
{
    public class BuffManager : MonoSingleton<BuffManager>
    {
        List<Buff> buffs;
        public void Register(Buff buff)
        {
            buffs.Add(buff);
        }
        public void Unregister(Buff buff)
        {
            buffs.Remove(buff);
        }
        private void Update()
        {
            foreach(Buff buff in buffs)
            {
                buff.Tick(Time.deltaTime);
            }
        }
    }
}