using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform rotateBase;
    public new Rigidbody rigidbody;
    public float speed;
    public float velocity;
    private float frazzTime;

    private void Update()
    {
        rotateBase.Rotate(Vector3.up, Input.GetAxis("Mouse X") * speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (frazzTime > 0)
        {
            frazzTime -= Time.deltaTime;
        }
        else
        {
            Vector3 diff =  GetDirection() * velocity - rigidbody.velocity;
            Vector3 force = diff * rigidbody.mass;
            rigidbody.AddForce(force , ForceMode.Impulse);
        }
    }

    public void Frazzing(float time)
    {
        frazzTime = time;
    }

    private Vector3 GetDirection()
    {
        return (Input.GetAxis("Vertical")* rotateBase.forward + Input.GetAxis("Horizontal") * rotateBase.right).normalized;
    }
}
