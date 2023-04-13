using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Signal : MonoBehaviour
{
    [SerializeField]Camera CharacterCamera;
    public void MoveCamera()
    {
        StartCoroutine(_MoveCamera());
        Debug.Log("111");
    }
    IEnumerator _MoveCamera()
    {
        CharacterCamera.transform.DOMove(new Vector3(1.64f,0.92f,3.82f),2f);
        yield return null;
    }
    public void BackCamera()
    {
        StartCoroutine(_BackCamera());
    }
    IEnumerator _BackCamera()
    {
        CharacterCamera.transform.DOLocalMove(new Vector3(0,0.8f,-0.8000003f),2f);
        yield return null;
    }
}
