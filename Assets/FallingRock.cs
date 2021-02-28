using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{

    public Vector3 rotationAngle;
    public float rotationSpeed;
    void OnTriggerEnter(Collider coll)
    {
        transform.position = new Vector3(transform.position.x, 100, transform.position.z);
    }

    void Update()
    {
        transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
    }
}
