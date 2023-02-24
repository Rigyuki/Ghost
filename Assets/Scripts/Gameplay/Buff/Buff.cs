using System.Collections;
using UnityEngine;
using Scripts.Gameplay.Basic;

namespace Scripts.Gameplay.BuffSystem
{
    public delegate void BuffCallback(Buff buff);
    public class Buff
    {
        public object source;
        public BuffType buffType;
        public float value;

        public bool isInfinity { get; private set; }
        public float lastTime { get; private set; }

        public bool isOneTime { get; private set; }
        public float interval { get; private set; }
        public float timeTillNextTrigger { get; private set; }

        public Buff(object source, BuffType buffType, float value = 0, float lastTime = -1, float interval = -1)
        {
            this.source = source;
            this.buffType = buffType;
            this.value = value;
            if (lastTime < 0)
            {
                this.isInfinity = true;
            }
            else
            {
                this.isInfinity = false;
                this.lastTime = lastTime;
            }
            if (interval < 0)
            {
                this.isOneTime = true;
            }
            else
            {
                this.isOneTime = false;
                this.interval = interval;
                this.timeTillNextTrigger = interval;
            }
        }

        BuffCallback OnStart;
        BuffCallback OnTrigger;
        BuffCallback OnFinish;

        public void AddOnStart(BuffCallback func) => OnStart += func;
        public void AddOnTrigger(BuffCallback func) => OnTrigger += func;
        public void AddOnFinish(BuffCallback func) => OnFinish += func;
        public void RemoveOnStart(BuffCallback func) => OnStart -= func;
        public void RemoveOnTrigger(BuffCallback func) => OnTrigger -= func;
        public void RemoveOnFinish(BuffCallback func) => OnFinish -= func;

        public void Start()
        {
            BuffManager.Instance.Register(this);
            OnStart?.Invoke(this);
        }

        public void Tick(float deltaTime)
        {
            if (isInfinity)
                return;
            lastTime -= deltaTime;
            if (lastTime <= 0)
            {
                Finish();
                return;
            }
            if (isOneTime)
                return;
            timeTillNextTrigger -= deltaTime;
            while (timeTillNextTrigger <= 0)
            {
                timeTillNextTrigger += interval;
                OnTrigger?.Invoke(this);
            }
        }

        public void Interrupt()
        {
            BuffManager.Instance.Unregister(this);
        }

        public void Finish()
        {
            BuffManager.Instance.Unregister(this);
            OnFinish?.Invoke(this);
        }

        public enum MergeType
        {
            AddTime,
            MaxTime,
        }

        public bool MergeBuff(Buff newBuff, MergeType mergeType, bool checkSource = true)
        {
            if (buffType != newBuff.buffType)
                return false;
            if (value != newBuff.value)
                return false;
            if (checkSource && source != newBuff.source)
                return false;
            switch (mergeType)
            {
                case MergeType.AddTime:
                    lastTime += newBuff.lastTime;
                    break;
                case MergeType.MaxTime:
                    lastTime = Mathf.Max(lastTime, newBuff.lastTime);
                    break;
            }
            return true;
        }
    }
}