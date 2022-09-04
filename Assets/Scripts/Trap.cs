using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float time;
    public string signal;

    private void OnTriggerEnter(Collider other)
    {
        other.BroadcastMessage(nameof(ISignalReceiver.TrapSignalReceiver), time, SendMessageOptions.DontRequireReceiver);
        SendMessageUpwards(nameof(ISignalReceiver.TrapSignalReceiver), signal, SendMessageOptions.DontRequireReceiver);
    }

    IEnumerator Trapping(Transform target)
    {
        float time = this.time;
        while (time > 0)
        {
            if(target.position != transform.position)
            {
                Vector3 position = Vector3.MoveTowards(target.position, transform.position, Time.deltaTime*10);
                position.y = target.position.y;
                target.position=position;
            }
            time -= Time.deltaTime;
            yield return null;
        }
    }
}
