using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{
    public int detection = 1;

    void OnTriggerEnter(Collider coll)
    {
        detection = 0;
    }

    void OnTriggerExit(Collider coll)
    {
        detection = 1;
    }
}