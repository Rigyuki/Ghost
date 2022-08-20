using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float speed;
    public Vector3 targetPos;
    Vector3 destination;

    private void Start()
    {
        destination = transform.position + targetPos;
    }

    private void Update()
    {
        if(transform.position != destination)
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        else
        {
            targetPos = -targetPos;
            destination += targetPos;
        }
    }
}
