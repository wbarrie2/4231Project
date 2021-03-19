using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTriggerZone : MonoBehaviour
{
    public GameObject lightOn;
    public GameObject lightOff;
    Collider block;
    bool inTrigger = false;

    void Update()
    {
        if (block != null)
        {
            block.GetComponent<PickUp>().grabbed = false;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        inTrigger = true;
        FlipLights(true);
        if (coll.GetComponent<PickUp>() != null)
        {
            block = coll;
        }
        else
        {
            block = null;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        inTrigger = false;
        FlipLights(false);
        block = null;
    }

    void FlipLights(bool input)
    {
        lightOn.SetActive(input);
        lightOff.SetActive(!input);
    }

    public bool InTrigger()
    {
        return inTrigger;
    }
}