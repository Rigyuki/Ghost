using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using Scripts.Gameplay.SceneChange;
using UnityEngine.PlayerLoop;
using Scripts.CustomTool.EditorTools;
using UnityEngine.SceneManagement;

public class keyAnim : MonoBehaviour
{
    [SerializeField] GameObject key;
    [SerializeField] GameObject _playerNew;
     
     
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(keyAni());
        }

    }

    IEnumerator keyAni()
    {
        key.transform.DOLocalRotate(new Vector3(0, 360, 0), 3f, RotateMode.WorldAxisAdd);
        key.transform.DOMove(new Vector3(-6f,1.7f,-8.4f), 2f);
        key.SetActive(false);
        _playerNew.transform.DOMove(new Vector3(-6.42f, 0.55f, -8.828f), 2f); 
        yield return null;
    }


}
