using Scripts.CustomTool.DesignPatterns.ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

namespace Scripts.Gameplay.Door
{
    public class DoorClose : MonoBehaviour
    {
        [SerializeField] private Transform doorLeft;
        [SerializeField] private Transform doorRight;

        public int area;

        private void OnEnable()
        {
            DoorCloseSubject.Instance.Register(OnMsg);
        }

        private void OnDisable()
        {
            DoorCloseSubject.Instance.Unregister(OnMsg);
        }

        //如果要引用这个请参照 luActive.cs
        private void OnMsg(object obj)
        {
            if (((int)obj) == area)
            {
                doorLeft.DORotate(new Vector3(0, -80f, 0), 1f, RotateMode.WorldAxisAdd);
                doorRight.DORotate(new Vector3(0, 80f, 0), 1f, RotateMode.WorldAxisAdd);
                this.enabled = false;
            }
        }
    }
}