using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Vector3 velocity { get; private set; }

    Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
    }

    void FixedUpdate()
    {
        var delta = transform.position - previousPosition;
        velocity = delta / Time.fixedDeltaTime;
        previousPosition = transform.position;
    }
}
