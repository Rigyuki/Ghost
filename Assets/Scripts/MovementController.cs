using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Serializable]
    public struct Movement
    {
        public bool isWorldSpace;
        public Vector3 origin;
        public Vector3 destination;
        public AnimationCurve curve;
    }

    public List<Movement> movements;
    public bool loop;

    private IEnumerator enumerator;
    private int index;

    // For Editor
    [Header("Add Key Frame")]
    public AnimationCurve curve;

    private void Update()
    {
        if (index < movements.Count)
        {
            if (enumerator == null)
            {
                Execute(movements[index++]);
            }

        }
        else if (loop)
        {
            index = 0;
        }
    }

    public void Execute(Movement movement)
    {
        AnimationCurve curve = movement.curve;
        Vector3 origin;
        Vector3 destination;
        if (movement.isWorldSpace)
        {
            origin = movement.origin;
            destination = movement.destination;
        }
        else
        {
            origin = transform.position;
            destination = transform.position + movement.destination;
        }
        enumerator = Move(origin, destination, curve);
        StartCoroutine(enumerator);
    }

    IEnumerator Move(Vector3 origin, Vector3 destination, AnimationCurve curve)
    {
        float timer = 0;
        float endTime = curve.keys[curve.keys.Length - 1].time;

        while (timer < endTime)
        {
            timer += Time.deltaTime;
            var value = curve.Evaluate(timer);
            transform.position = Vector3.Lerp(origin, destination, value);
            yield return null;
        }
        enumerator = null;
    }
}
