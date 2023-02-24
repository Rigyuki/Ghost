using Scripts.Gameplay.BuffSystem;
using Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Basic
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : CharacterBase
    {
        CharacterController controller;

        #region movement

        [Header("移动")]
        public float speed = 2;
        public float speedFallout = 20;
        public float jumpSpeed = 10;
        public Vector3 Axis => (Input.GetAxis("Vertical") * transform.right - Input.GetAxis("Horizontal") * transform.forward).normalized;
        public bool JumpPressed => Input.GetKey(KeyCode.Space);
        Vector3 horizontalVelocity = Vector3.zero;
        Vector3 currentVelocity = Vector3.zero;
        Vector3 targetVelocity;

        int facing = 1;//1=前，2=后;4=左，8=右

        [Header("地面检测")]
        public LayerMask groundLayers;
        public float groundTestDistance = 2f;

        #endregion

        #region Buff
        protected override void InitBuffCallback(Buff buff)
        {
            switch(buff.buffType)
            {
                case BuffType.Frozen:
                    buff.AddOnStart(Freeze);
                    buff.AddOnFinish(Unfreeze);
                    break;
                case BuffType.SpeedChange:
                    buff.AddOnStart(SpeedChange);
                    buff.AddOnFinish(SpeedRevert);
                    break;
            }
        }

        bool frozen = false;
        float speedMultiplier = 1;
        int slowed = 0;

        void Freeze(Buff buff)
        {
            frozen = true;
        }
        void Unfreeze(Buff buff)
        {
            frozen = false;
        }
        void SpeedChange(Buff buff)
        {
            speedMultiplier *= buff.value;
        }
        void SpeedRevert(Buff buff)
        {
            speedMultiplier /= buff.value;
        }
        #endregion

        #region dash
        [Header("冲刺")]
        public bool unlockDash = false;
        bool dashing = false;
        Coroutine dashCoroutine;
        [SerializeField] float dashSpeed = 4;
        [SerializeField] float dashTime = 0.5f;
        [SerializeField] KeyCode dashKey = KeyCode.F;
        public bool DashPressed => unlockDash && Input.GetKeyDown(dashKey);

        void Dash()
        {
            if (dashing || !DashPressed)
                return;
            dashCoroutine = StartCoroutine(DashCoroutine());
        }
        IEnumerator DashCoroutine()
        {
            dashing = true;
            yield return new WaitForSeconds(dashTime);
            dashing = false;
        }
        #endregion

        #region interact
        [SerializeField] KeyCode interactKey = KeyCode.E;
        #endregion

        #region move object
        [Header("移花接木")]
        public bool unlockMove = true;
        GameObject targetObject;
        public string movableTag;
        public LayerMask putLayer;
        public float putOffsetY;
        void PickOrPut()
        {
            if (!unlockMove || !Input.GetMouseButtonDown(0)) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (targetObject)
            {
                Physics.Raycast(ray, out hit, putLayer);
                targetObject.transform.position = hit.point + Vector3.up * putOffsetY;
                targetObject = null;
            }
            else
            {
                Physics.Raycast(ray, out hit);
                if (hit.collider.gameObject.CompareTag(movableTag))
                {
                    targetObject = hit.collider.gameObject;
                }
            }
        }
        #endregion

        void Awake()
        {
            Init();
        }

        protected override void Init()
        {
            base.Init();
            controller = GetComponent<CharacterController>();
        }

        void ChangeFacing(Vector3 dir)
        {
            if (dir.magnitude == 0)
                return;
            facing = 0;
            if (dir.x > 0)
                facing |= 1;
            else if (dir.x < 0)
                facing |= 2;
            if (dir.z > 0)
                facing |= 4;
            else if (dir.z < 0)
                facing |= 8;
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

        void Update()
        {
            targetVelocity = Vector3.zero;
            var axis = Axis;
            ChangeFacing(axis);
            CheckPlatform();
            Dash();
            if (!frozen)
            {
                PickOrPut();
                if (!dashing)
                    targetVelocity += axis * speed * speedMultiplier;
                else
                {
                    Vector3 f = Vector3.zero;
                    if ((facing & 1) != 0)
                        ++f.x;
                    if ((facing & 2) != 0)
                        --f.x;
                    if ((facing & 4) != 0)
                        ++f.z;
                    if ((facing & 8) != 0)
                        --f.z;
                    targetVelocity += f.normalized * dashSpeed * speedMultiplier;
                }
                horizontalVelocity = horizontalVelocity.Fallout(targetVelocity, speedFallout);
            }
            else
            {
                horizontalVelocity = targetVelocity;
            }

            currentVelocity = new Vector3(horizontalVelocity.x, currentVelocity.y, horizontalVelocity.z);

            if (!dashing)
            {
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
            }
            else
            {
                currentVelocity.y = 0;
            }
            controller.Move(currentVelocity * Time.deltaTime);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(transform.position + GetComponent<CharacterController>().center, Vector3.down * groundTestDistance);
        }
    }
}