using System.Collections;
using UnityEngine;

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
        public Transform middle;
        public Transform innermost;
        public float speed;

        private void OnEnable()
        {
            TrigramRotateSubject.Instance.Register(Rotate);
        }
        private void OnDisable()
        {
            TrigramRotateSubject.Instance.Unregister(Rotate);
        }

        private IEnumerator Rotate(Transform transform, Vector3 axis, float angle)
        {
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
            if(CheckUnlock())
            {
                TrigramUnlockSubject.Instance.Notify(null);
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
            //TODO: ∞Àÿ‘’ÛΩ‚À¯≈–∂®
            return false;
        }
    }
}