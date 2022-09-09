using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trap : MonoBehaviour
{
    public UnityEvent _event;

    private void OnTriggerEnter(Collider other)
    {
        _event.Invoke();
    }

    //IEnumerator Trapping(GameObject target, float time)
    //{
    //    while (time > 0)
    //    {
    //        if(target.transform.position != transform.position)
    //        {
    //            Vector3 position = Vector3.MoveTowards(target.transform.position, transform.position, Time.deltaTime*10);
    //            position.y = target.transform.position.y;
    //            target.transform.position=position;
    //        }
    //        time -= Time.deltaTime;
    //        yield return null;
    //    }
    //}
}
