using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Basic
{
    [System.Obsolete]
    public class UltraPlayer : MonoBehaviour
    {
        public LayerMask groundLayers;
        public float groundTestDistance = 2;

        CharacterController controller;
        public bool isOnGround { get; private set; }
        public Vector3 baseVelocity { get; private set; }

        void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            TestGround();
        }

        void TestGround()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position + controller.center, -Vector3.up);
            Debug.DrawRay(ray.origin, ray.direction * groundTestDistance);
            isOnGround = Physics.Raycast(ray, out hit, groundTestDistance, groundLayers);

            var platform = hit.collider?.GetComponentInChildren<Platform>();
            if (platform)
            {
                // controller.SimpleMove(platform.velocity);
                baseVelocity = platform.velocity;
            }
            else
            {
                baseVelocity = Vector3.zero;
            }
        }
    }
}