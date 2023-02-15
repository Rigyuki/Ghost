using Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Basic
{
    public class PlayerController : MonoBehaviour
    {
        [Header("速度")]
        public float speed = 2;
        public float speedFallout = 20;
        public float jumpSpeed = 10;
        [Header("地面")]
        public LayerMask groundLayers;
        public float groundTestDistance = 2f;
        public Vector3 Axis => (Input.GetAxis("Vertical") * transform.right - Input.GetAxis("Horizontal") * transform.forward).normalized;
        public bool JumpPressed => Input.GetKey(KeyCode.Space);

        CharacterController controller;
        Vector3 horizontalVelocity = Vector3.zero;
        Vector3 currentVelocity = Vector3.zero;
        Vector3 targetVelocity;

        bool frozen = false;
        Coroutine freezeCoroutine;

        void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            targetVelocity = Vector3.zero;
            CheckPlatform();
            if (!frozen)
            {
                var axis = Axis;
                targetVelocity += axis * speed;
            }
            horizontalVelocity = horizontalVelocity.Fallout(targetVelocity, speedFallout);

            currentVelocity = new Vector3(horizontalVelocity.x, currentVelocity.y, horizontalVelocity.z);
            currentVelocity += Physics.gravity * Time.deltaTime;

            if (controller.isGrounded)
            {
                if (!frozen && JumpPressed)
                {
                    currentVelocity.y = jumpSpeed;
                }
                else
                {
                    currentVelocity.y = 0;
                }
            }
            controller.Move(currentVelocity * Time.deltaTime);
        }

        void CheckPlatform()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position + controller.center, Vector3.down);
            Physics.Raycast(ray, out hit, groundTestDistance, groundLayers);

            var platform = hit.collider?.GetComponentInChildren<Platform>();

            if (platform)
            {
                targetVelocity = platform.velocity;
            }
            else
            {
                ;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(transform.position + GetComponent<CharacterController>().center, Vector3.down * groundTestDistance);
        }

        #region Frozen
        void Freeze(float duration)
        {
            if (freezeCoroutine != null) StopCoroutine(freezeCoroutine);
            freezeCoroutine = StartCoroutine(FreezeCoroutine(duration));
        }

        IEnumerator FreezeCoroutine(float duration)
        {
            frozen = true;
            yield return new WaitForSeconds(duration);
            frozen = false;
        }
        #endregion
    }
}