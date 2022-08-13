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
        rigidbody.AddForce(force * Input.GetAxis("Vertical") * rotateBase.forward);
        rigidbody.AddForce(force * Input.GetAxis("Horizontal") * rotateBase.right);
    }

    //rotateBase.Rotate(Vector3.up, Input.GetAxis("Rotate") * speed);
}
