using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Gameplay.Chase
{
    public class Opera : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip clip1;
        public AudioClip clip2;
        void Start()
        {
            StartCoroutine(PlayOpera());
        }
        IEnumerator PlayOpera()
        {
            yield return new WaitForSeconds(1f);
            audioSource.clip = clip1;
            audioSource.Play();
            while (audioSource.isPlaying)
                yield return null;
            audioSource.clip = clip2;
            audioSource.Play();
            while (audioSource.isPlaying)
                yield return null;
            Destroy(audioSource);
            Destroy(this);
        }
    }
}