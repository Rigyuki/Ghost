using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float duration;
    public Vector3 delta;

    [Tooltip("Normalized movement curve over time (from 0 to 1)")]
    public AnimationCurve curve;

    Vector3 origin;
    Vector3 destination;
    float time = 0;
    bool state = false;

    private void Start()
    {
        origin = transform.position;
        destination = origin + delta;
    }

    private void Update()
    {
        if (!state)
        {
            transform.position = origin;
        }
        else
        {
            var factor = curve.Evaluate(time);
            transform.position = Vector3.Lerp(origin, destination, factor);
        }
    }

    public void Activate() => state = true;
}
