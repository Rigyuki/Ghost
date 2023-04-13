using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.CustomTool.DesignPatterns.ObserverPattern;

namespace Scripts.Gameplay.Chase
{
    public class ChaseEnder : MonoBehaviour
    {
        bool triggered;
        private void OnTriggerEnter(Collider other)
        {
            if (triggered)
                return;
            triggered = true;
            StartCoroutine(WaitSomeTime());
        }
        IEnumerator WaitSomeTime()
        {
            yield return new WaitForSeconds(1f);
            MsgCenterByList.SendMessage(new CommonMsg
            {
                MsgId = MsgCenterByList.ENDING,
            });
            Destroy(this);
        }
    }
}