using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using PixelCrushers.DialogueSystem;


namespace Scripts.Gameplay.MoveObject 
{
    public class MoveObject : MonoBehaviour
    {
        [SerializeField] GameObject Mountain;
        [SerializeField] GameObject Stone;
        [SerializeField] GameObject Brige1;
        [SerializeField] GameObject Brige2;
        [SerializeField] GameObject BrigeCollider;
        [SerializeField] DialogueDatabase dialogue;
        public void moveMountain()
        {
            StartCoroutine(_movingMountain());
            Debug.Log("moving");
        }
        public void moveStone()
        {
            StartCoroutine(SetBoolBrige());
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
            Stone.transform.DOMove(new Vector3(1.95f, -0.05f, 4.02f), 2f);
            yield return null;
        }
        IEnumerator SetBoolBrige()
        {
            yield return new WaitForSeconds(2f);
            Brige1.SetActive(false);
            Brige2.SetActive(true);
            BrigeCollider.SetActive(false);
            
        }        
    }

}
