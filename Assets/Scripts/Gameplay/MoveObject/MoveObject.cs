using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

namespace Scripts.Gameplay.MoveObject 
{
    public class MoveObject : MonoBehaviour
    {
        [SerializeField] GameObject Mountain;
        public void moveMountain()
        {
            StartCoroutine(_movingMountain());
        }

        IEnumerator _movingMountain()
        {
            Mountain.transform.DOMove(new Vector3(-2.33f, 0.13f, -0.97f), 2f);
            yield return null;
        }
    }

}
