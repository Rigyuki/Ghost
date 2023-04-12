using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigramTip : MonoBehaviour
{
    [SerializeField] GameObject tip;
    bool triggered;
    private void OnTriggerEnter(Collider other)
    {
        if (triggered||other.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;
        StartCoroutine(Tip());
    }
    IEnumerator Tip()
    {
        tip.SetActive(true);
        triggered = true;
        while (!Input.GetMouseButtonUp(0))
            yield return null;
        tip.SetActive(false);
        triggered = false;
    }
}