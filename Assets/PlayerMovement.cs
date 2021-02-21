using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    Vector3 move;

    private float x;
    private float z;

    bool isGrounded = false;

    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            Debug.Log(x);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -5f;
        }

        GetSmoothAxisRaw("Horizontal", ref x);
        GetSmoothAxisRaw("Vertical", ref z);

        move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.unscaledDeltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
        }

        velocity.y += gravity * Time.unscaledDeltaTime;

        controller.Move(velocity * Time.unscaledDeltaTime);
    }
    private void GetSmoothAxisRaw(string name, ref float axis)
    {
        var r = Input.GetAxisRaw(name);

        if (r != 0)
        {
            axis = Mathf.Clamp(axis + r * 5 * Time.unscaledDeltaTime, -1f, 1f);
        }
        else
        {
            axis = Mathf.Clamp01(Mathf.Abs(axis) - 3 * Time.unscaledDeltaTime) * Mathf.Sign(axis);
        }
    }
}