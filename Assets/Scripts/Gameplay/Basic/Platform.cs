using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Basic
{
    public class Platform : MonoBehaviour
    {
        public Vector3 velocity { get; private set; }

        Vector3 previousPosition;

        void Start()
        {
            previousPosition = transform.position;
        }

        //void FixedUpdate()
        //{
        //    var delta = transform.position - previousPosition;
        //    velocity = delta / Time.fixedDeltaTime;
        //    previousPosition = transform.position;
        //}
        void Update()
        {
            var delta = transform.position - previousPosition;
            velocity = delta / Time.deltaTime;
            previousPosition = transform.position;
        }
    }
}