using Scripts.CustomTool.DesignPatterns.ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

namespace Scripts.Gameplay.Door
{
    public class DoorOpen : MonoBehaviour
    {
        [SerializeField] private Transform doorLeft;
        [SerializeField] private Transform doorRight;

        private void OnEnable()
        {
            MsgCenterByList.AddListener(OnMsg);
        }

        private void OnDisable()
        {
            MsgCenterByList.RemoveListener(OnMsg);
        }

        //如果要引用这个请参照 luActive.cs
        private void OnMsg(CommonMsg obj)
        {
            if (obj.MsgId == MsgCenterByList.SAFE_DOOR_OPEN)
            {
                doorLeft.DORotate(new Vector3(0, 80f, 0), 3f, RotateMode.WorldAxisAdd);
                doorRight.DORotate(new Vector3(0, -80f, 0), 3f, RotateMode.WorldAxisAdd);
            }
            this.enabled = false;
        }
    }
}