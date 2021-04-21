using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public AudioSource soundEffect;

    AudioClip terrainClip;
    AudioClip groundClip;

    public float speed = 12f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;

    public Transform groundCheck;
    LayerMask groundMask;
    LayerMask boxMask;
    LayerMask terrainMask;

    Vector3 velocity;
    Vector3 move;

    private float x;
    private float z;

    bool isGrounded = false;
    bool onTerrain = false;
    bool onGround = false;

    void Start()
    {
        groundMask = LayerMask.GetMask("Ground");
        boxMask = LayerMask.GetMask("Box");
        terrainMask = LayerMask.GetMask("Terrain");

        terrainClip = Resources.Load<AudioClip>("Grass");
        groundClip = Resources.Load<AudioClip>("Concrete");
    }

    void Update()
    {
        onTerrain = Physics.CheckSphere(groundCheck.position, groundDistance, terrainMask);
        onGround =
            (
                Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) ||
                Physics.CheckSphere(groundCheck.position, groundDistance, boxMask)
            );

        isGrounded = onGround || onTerrain;

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

        if (move.x != 0 && move.z != 0 && isGrounded && Time.timeScale > .5)
        {
            if (onTerrain)
            {
                if (soundEffect.clip != terrainClip)
                {
                    soundEffect.clip = terrainClip;
                }
            }
            else if (onGround)
            {
                if (soundEffect.clip != groundClip)
                {
                    soundEffect.clip = groundClip;
                }
            }

            if (!soundEffect.isPlaying)
            {
                soundEffect.Play();
            }
        }
        else
        {
            soundEffect.Pause();
        }
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

    public Vector3 GetVelocity()
    {
        return controller.velocity;
    }
}