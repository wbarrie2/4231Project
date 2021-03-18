using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject dest;
    public bool grabbed = false;
    Vector3 lastPosition;

    void Start()
    {
        lastPosition = this.transform.position;
    }

    void OnMouseDown()
    {
        if (Vector3.Distance(dest.transform.position, this.transform.position) <= 5)
        {
            grabbed = true;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    void OnMouseUp()
    {
        grabbed = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().freezeRotation = false;
    }

    void Update()
    {
        if (grabbed)
        {
            if (!dest.GetComponent<DestScript>().inWall)
            {
                this.transform.position = dest.transform.position;
                this.transform.rotation = Quaternion.Euler(0, dest.transform.eulerAngles.y, 0);
                lastPosition = this.transform.position;
            }
            else
            {
                this.transform.position = lastPosition;
            }
        }
    }
}