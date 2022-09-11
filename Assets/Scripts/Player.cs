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
    private float frazzTimer;
    public float jumpForce;
    public float jumpTime;
    private float jumpTimer;

    private void Update()
    {
        rotateBase.Rotate(Vector3.up, Input.GetAxis("Mouse X") * speed * Time.deltaTime);
        if (jumpTimer <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpTimer = jumpTime;
            }
        }
        else
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (frazzTimer > 0)
        {
            frazzTimer -= Time.deltaTime;
        }
        else
        {
            Vector3 direction = GetDirection();
            if (direction != Vector3.zero)
            {
                Vector3 diff = direction * velocity - new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
                Vector3 force = diff * rigidbody.mass;
                rigidbody.AddForce(force, ForceMode.Impulse);
            }
        }
    }

    private Vector3 GetDirection()
    {
        return (Input.GetAxis("Vertical") * rotateBase.forward + Input.GetAxis("Horizontal") * rotateBase.right).normalized;
    }

    public void Frazz(float time)
    {
        frazzTimer = time;
    }
}
