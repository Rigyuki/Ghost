using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    public Transform outermost;
    public Transform middle;
    public Transform innermost;
    public float speed;

    private IEnumerator Rotate(Transform transform, Vector3 axis, float angle)
    {
        while (angle > 0)
        {
            float rotate = speed * Time.deltaTime;
            if (rotate < angle)
            {
                angle -= rotate;
            }
            else
            {
                rotate = angle;
                angle = 0;
            }
            transform.Rotate(axis, rotate);
            yield return null;
        }
    }

    public void RotateOuter() => StartCoroutine(Rotate(outermost, Vector3.up, 45));
    public void RotateMiddle() => StartCoroutine(Rotate(middle, Vector3.up, 45));
    public void RotateInner() => StartCoroutine(Rotate(innermost, Vector3.up, 45));
}
