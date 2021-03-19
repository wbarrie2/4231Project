using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject dest;
    public GameObject player;
    public bool grabbed = false;
    Rigidbody thisBody;
    Vector3 lastPosition;

    void Start()
    {
        lastPosition = this.transform.position;
        thisBody = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        if (Vector3.Distance(dest.transform.position, this.transform.position) <= 5)
        {
            grabbed = true;
            thisBody.useGravity = false;
            thisBody.freezeRotation = true;
        }
    }

    void OnMouseUp()
    {
        grabbed = false;
        thisBody.useGravity = true;
        thisBody.freezeRotation = false;
        thisBody.velocity = player.GetComponent<PlayerMovement>().GetVelocity();
    }

    void Update()
    {
        if (grabbed)
        {
            if (!dest.GetComponent<DestScript>().inWall)
            {
                lastPosition = this.transform.position;
                this.transform.position = dest.transform.position;
                this.transform.rotation = Quaternion.Euler(0, dest.transform.eulerAngles.y, 0);
            }
            else
            {
                this.transform.position = lastPosition;
            }
        }
        else
        {
            lastPosition = this.transform.position;
        }
    }
}