using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Basic
{
    [System.Obsolete]
    public class TempController : MonoBehaviour
    {
        new Rigidbody rigidbody;

        public float force;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }


        void Update()
        {
            if (Input.GetButton("Horizontal"))
            {
                rigidbody.AddForce(force * Input.GetAxisRaw("Horizontal") * Vector3.right);
            }
            if (Input.GetButton("Vertical"))
            {
                rigidbody.AddForce(force * Input.GetAxisRaw("Vertical") * Vector3.forward);
            }
        }
    }
}