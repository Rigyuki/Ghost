using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class ChangeCameraAreaEnter : MonoBehaviour
{
    [SerializeField] Camera m_camera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {           
            m_camera.DOOrthoSize(5.7f, 2f);
        }       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            m_camera.DOOrthoSize(5.7f, 2f);
        }       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            m_camera.DOOrthoSize(2.5f, 2f);
        }
    }
}
