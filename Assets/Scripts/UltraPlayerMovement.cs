using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraPlayerMovement : MonoBehaviour
{
    public float speed = 2;
    public float speedFallout = 20;

    public float jumpSpeed = 10;

    CharacterController controller;
    UltraPlayer player;
    Vector3 horizontalVelocity = Vector3.zero;
    Vector3 currentVelocity = Vector3.zero;

    bool frozen = false;
    Coroutine freezeCoroutine;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        player = GetComponent<UltraPlayer>();
    }

    void Update()
    {
        var targetVelocity = player.baseVelocity;
        if (!frozen)
        {
            var axis = Axis;
            targetVelocity += axis * speed;
        }
        horizontalVelocity = horizontalVelocity.Fallout(targetVelocity, speedFallout);

        currentVelocity = new Vector3(horizontalVelocity.x, currentVelocity.y, horizontalVelocity.z);
        currentVelocity += Physics.gravity * Time.deltaTime;

        if (controller.isGrounded)
        {
            currentVelocity.y = 0;
        }

        if (!frozen && JumpPressed && controller.isGrounded)
        {
            currentVelocity.y = jumpSpeed;
        }

        controller.Move(currentVelocity * Time.deltaTime);
    }

    public Vector3 Axis => (Input.GetAxis("Vertical") * transform.right - Input.GetAxis("Horizontal") * transform.forward).normalized;
    public bool JumpPressed => Input.GetKey(KeyCode.Space);

    void Freeze(float duration)
    {
        if (freezeCoroutine != null) StopCoroutine(freezeCoroutine);
        freezeCoroutine = StartCoroutine(FreezeCoroutine(duration));
    }

    IEnumerator FreezeCoroutine(float duration)
    {
        frozen = true;
        yield return new WaitForSeconds(duration);
        frozen = false;
    }
}
