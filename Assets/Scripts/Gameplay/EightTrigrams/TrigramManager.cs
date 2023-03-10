using System.Collections;
using UnityEngine;
using Scripts.CustomTool.DesignPatterns.ObserverPattern;

namespace Scripts.Gameplay.EightTrigrams
{
    public enum TrigramRing
    {
        INNER,
        MIDDLE,
        OUTER,
    }

    public class TrigramManager : MonoBehaviour
    {
        public Transform outermost;
        public float outerOffset;
        public Transform middle;
        public float middleOffset;
        public Transform innermost;
        public float innerOffset;
        public float speed;

        private void OnEnable()
        {
            TrigramRotateSubject.Instance.Register(Rotate);
        }
        private void OnDisable()
        {
            TrigramRotateSubject.Instance.Unregister(Rotate);
        }
        bool rotating;
        private IEnumerator Rotate(Transform transform, Vector3 axis, float angle)
        {
            if (rotating)
                yield break;
            rotating = true;
            while (angle > 0)
            {
                float rotate = speed * Time.deltaTime;
                if (rotate < angle)
                {
                    angle -= rotate;
                }
                else
                {
                    rotate = angle;
                    angle = 0;
                }
                transform.Rotate(axis, rotate);
                yield return null;
            }
            rotating = false;
            if (CheckUnlock())
            {
                TrigramUnlockSubject.Instance.Notify(null);
                MsgCenterByList.SendMessage(new CommonMsg()
                {
                    MsgId = MsgCenterByList.SAFE_DOOR_OPEN
                });
            }
        }
        void RotateOuter() => StartCoroutine(Rotate(outermost, Vector3.up, 45));
        void RotateMiddle() => StartCoroutine(Rotate(middle, Vector3.up, 45));
        void RotateInner() => StartCoroutine(Rotate(innermost, Vector3.up, 45));
        void Rotate(object ring)
        {
            switch ((TrigramRing)ring)
            {
                case TrigramRing.INNER:
                    RotateInner();
                    break;
                case TrigramRing.MIDDLE:
                    RotateMiddle();
                    break;
                case TrigramRing.OUTER:
                    RotateOuter();
                    break;
            }
        }
        bool CheckUnlock()
        {
            int o = (Mathf.RoundToInt(outermost.localEulerAngles.y + outerOffset) % 360 + 360) % 360;
            int m = (Mathf.RoundToInt(middle.localEulerAngles.y + middleOffset) % 360 + 360) % 360;
            int i = (Mathf.RoundToInt(innermost.localEulerAngles.y + innerOffset) % 360 + 360) % 360;
            return o == m && m == i;
        }
    }
}