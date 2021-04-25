using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    bool start = false;
    Vector3 moveTo;
    int moveSpeed;

    void Update()
    {
        if (start)
        {
            if (Vector3.Distance(transform.position, moveTo) > 0.1f)
            {
                transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void StartCredits(float inputMove, int inputSpeed)
    {
        start = true;
        moveTo = new Vector3(transform.position.x, inputMove, transform.position.z);
        moveSpeed = inputSpeed;
    }
}