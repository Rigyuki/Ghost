using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float time;

    private void OnTriggerEnter(Collider other)
    {
        other.BroadcastMessage(nameof(Player.Frazzing), time);
        //StartCoroutine(Trapping(other.transform));
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
