using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Gameplay.Basic;

namespace Scripts.Gameplay.EnemyAI
{
    [RequireComponent(typeof(Collider))]
    public class EyeController : MonoBehaviour
    {
        [SerializeField] AnimationPlayer eye;
        [SerializeField] Transform ghostContainer;
        [SerializeField] AnimationPlayer ghost;
        [SerializeField] string eyeCloseAnim;
        [SerializeField] string eyeOpenAnim;
        Coroutine ghostCoroutine;
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
            eye.Play(0, eyeOpenAnim, false, false);
            if (ghostCoroutine != null)
                StopCoroutine(ghostCoroutine);
            ghostCoroutine = StartCoroutine(GhostAppear(0.2f));
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
            eye.Play(0, eyeCloseAnim, false, false);
            if (ghostCoroutine != null)
                StopCoroutine(ghostCoroutine);
            ghostCoroutine = StartCoroutine(GhostDisappear(0.2f));
        }
        IEnumerator GhostAppear(float duration)
        {
            float fullTime = duration;
            duration = (1 - ghostContainer.localScale.x) * fullTime;
            while (duration > 0)
            {
                duration -= Time.deltaTime;
                ghostContainer.localScale = Vector3.one * (1 - duration / fullTime);
                yield return null;
            }
            ghostContainer.localScale = Vector3.one;
        }
        IEnumerator GhostDisappear(float duration)
        {
            float fullTime = duration;
            duration = ghostContainer.localScale.x * fullTime;
            while (duration > 0)
            {
                duration -= Time.deltaTime;
                ghostContainer.localScale = Vector3.one * (duration / fullTime);
                yield return null;
            }
            ghostContainer.localScale = Vector3.zero;
        }
    }
}