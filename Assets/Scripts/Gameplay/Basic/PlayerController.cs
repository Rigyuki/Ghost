using Scripts.Gameplay.BuffSystem;
using Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityGameFramework.Runtime;
using Scripts.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace Scripts.Gameplay.Basic
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : CharacterBase
    {
        public Axis AxisTest;
        CharacterController controller;

        bool hit;
        bool dead;

        [SerializeField] GameObject dialogueBox;
        bool dialogueOngoing => dialogueBox && dialogueBox.activeSelf;

        #region movement

        [Header("移动")]
        public float speed = 2;
        public float speedFallout = 20;
        public float jumpSpeed = 10;

        //todo:移动入口
        public Vector3 Axis => dialogueOngoing ? Vector3.zero :
            (transform.rotation == new Quaternion(0, -1, 0, 0)
            ? (Input.GetAxis("Vertical") * (-1) * transform.right - Input.GetAxis("Horizontal") * (-1) * transform.forward).normalized
           : (Input.GetAxis("Vertical") * transform.right - Input.GetAxis("Horizontal") * transform.forward).normalized);
        
        
        //public Vector3 Axis => (Input.GetAxis("Vertical") * transform.right - Input.GetAxis("Horizontal") * transform.forward).normalized;

       
        public bool JumpPressed => Input.GetKey(KeyCode.Space) && !dialogueOngoing;
        Vector3 horizontalVelocity = Vector3.zero;
        Vector3 currentVelocity = Vector3.zero;
        Vector3 targetVelocity;
        bool walking;

        int facing = 1;//1=前，2=后;4=左，8=右

        [Header("地面检测")]
        public LayerMask groundLayers;
        public float groundTestDistance = 2f;
        bool grounded = true;

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
            animationPlayer.Play(0, hit_base, facing, false, true);
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
        public bool DashPressed => unlockDash && Input.GetKeyDown(dashKey) && !dialogueOngoing;

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
        [Header("交互")]
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
            if (dialogueOngoing || !unlockMove || !Input.GetMouseButtonDown(0)) return;
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
                if (hit.collider && hit.collider.gameObject.CompareTag(movableTag))
                {
                    targetObject = hit.collider.gameObject;
                }
            }
        }
        #endregion

        #region animation
        [Header("动画")]
        [SerializeField] AnimationPlayer animationPlayer;
        public string idle_base = "daoshi_stand";
        public string walk_base = "daoshi_walk";
        public string jump_base = "daoshi_jump";
        public string run_base = "daoshi_run";
        public string hit_base = "daoshi_shouji";
        public string die_animation = "daoshi_die_135";
        void SetAnimation()
        {
            if (frozen)
                return;
            if (dashing)
                animationPlayer.Play(0, run_base, facing, true);
            else if (!grounded)
                animationPlayer.Play(0, jump_base, facing, false);
            else if (walking)
                animationPlayer.Play(0, walk_base, facing, true);
            else
                animationPlayer.Play(0, idle_base, facing, true);
        }
        #endregion

        #region Audio
        [Header("声音")]
        [SerializeField] AudioSource WalkAudio;
        [SerializeField]AudioSource RunAudio;
        [SerializeField] AudioSource HitAudio;
        public AudioClip hitClip;
        [SerializeField]AudioSource JumpAudio;
        void SetAudio()
        {
            if(frozen)
                return;
            if(walking&&!WalkAudio.isPlaying)
                WalkAudio.Play();
            else if(dashing&&!RunAudio.isPlaying)
            {
                if(WalkAudio.isPlaying)
                    WalkAudio.Pause();
                RunAudio.Play();
            }
            else if(!grounded&&!JumpAudio.isPlaying)
            {
                if(WalkAudio.isPlaying)
                    WalkAudio.Pause();
                JumpAudio.Play();
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
            //todo:跳跃鉴定
            Ray ray = new Ray(transform.position + controller.center, Vector3.down);
            grounded = Physics.Raycast(ray, out hit, groundTestDistance, groundLayers);

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
            
            walking = axis.magnitude != 0;
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

            if (dead)
                return;

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
            SetAnimation();
            SetAudio();
            controller.Move(currentVelocity * Time.deltaTime);
        }

        private void LateUpdate()
        {
            if(transform.position.y<-10)
            {
                TakeDamage(10000);
            }
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            HPChangeSubject.Instance.Notify(1f * currentHP / maxHP);
            if (!dead)
            {
                animationPlayer.Play(0, hit_base, facing, false, true);
                if (HitAudio)
                {
                    HitAudio.clip = hitClip;
                    HitAudio.Play();
                }
            }
            // audioSource.clip = hitClip;
            // audioSource.Play();
        }

        public override void Die()
        {
            if (dead)
                return;
            dead = true;
            base.Die();
            animationPlayer.Play(0, die_animation, false, true);
            StartCoroutine(RestartLevel(2));
        }
        IEnumerator RestartLevel(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(gameObject.scene.name);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(transform.position + GetComponent<CharacterController>().center, Vector3.down * groundTestDistance);
        }
    }
}