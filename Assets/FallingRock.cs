using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    Vector3 rotationAngle;
    float rotationSpeed;
    DissolveScript dissolve;
    public bool useSpawnPoint = false;
    void Start()
    {
        rotationAngle = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
        GetComponent<Rigidbody>().drag = Random.Range(0.5f, 0.1f);
        rotationSpeed = Random.Range(1, 5);
        if (useSpawnPoint)
        {
            dissolve = GetComponent<DissolveScript>();
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (useSpawnPoint)
        {
            dissolve.StartDissolve();
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 100, transform.position.z);
        }
        
    }

    void Update()
    {
        transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
    }
}