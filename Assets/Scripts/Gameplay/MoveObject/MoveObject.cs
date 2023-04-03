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
        [SerializeField] GameObject Stone;
        public void moveMountain()
        {
            StartCoroutine(_movingMountain());
            Debug.Log("moving");
        }
        public void moveStone()
        {
            StartCoroutine(_movingStone());
            Debug.Log("moving11");
        }
        IEnumerator _movingMountain()
        {
            Mountain.transform.DOMove(new Vector3(-2.33f, 0.13f, -0.97f), 2f);
            yield return null;
        }
        IEnumerator _movingStone()
        {
            Mountain.transform.DOMove(new Vector3(-2.33f, 0.13f, -0.97f), 2f);
            yield return null;
        }        
    }

}
