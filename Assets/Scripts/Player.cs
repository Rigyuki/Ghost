using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform rotateBase;
    public new Rigidbody rigidbody;
    public float speed;
    public float force;

    private void FixedUpdate()
    {
        rotateBase.Rotate(Vector3.up, Input.GetAxis("Mouse X") * speed);
        rigidbody.AddForce(rotateBase.forward * Input.GetAxis("Vertical") * force);
        rigidbody.AddForce(rotateBase.right * Input.GetAxis("Horizontal") * force);
    }

    //rotateBase.Rotate(Vector3.up, Input.GetAxis("Rotate") * speed);
}
