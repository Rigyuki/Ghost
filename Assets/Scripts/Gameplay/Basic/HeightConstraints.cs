using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Basic
{
    public class HeightConstraints : MonoBehaviour
    {
        public float higherConstraint;
        public float lowerConstraint;
        void LateUpdate()
        {
            Vector3 pos = transform.position;
            pos.y = Mathf.Max(Mathf.Min(pos.y, higherConstraint), lowerConstraint);
            transform.position = pos;
        }
    }
}