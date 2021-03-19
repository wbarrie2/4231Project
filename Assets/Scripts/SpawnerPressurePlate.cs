using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPressurePlate : MonoBehaviour
{
    public GameObject plateLightOn;
    public GameObject plateLightOff;
    public float movePlateTo;
    public float plateMoveSpeed;
    public int activationScale;
    float activated = 0;
    float originalY;
    bool inTrigger;
    //Plate Variables

    public GameObject block;
    //Block Variables

    void Start()
    {
        originalY = transform.position.y;
    }

    void Update()
    {

        if (activated < 5)
        {
            if (transform.position.y < originalY)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + plateMoveSpeed * Time.deltaTime, transform.position.z);
            }
        }

        if (activated > 0)
        {
            activated -= activationScale * Time.deltaTime;
        }
        else if (activated < 0)
        {
            activated = 0;
            FlipLights(false);
            transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
        }

        if (inTrigger && activated < 5)
        {
            activated = 5;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        transform.position = new Vector3(transform.position.x, movePlateTo, transform.position.z);
        activated = 10;
        inTrigger = true;
        FlipLights(true);
        block.GetComponent<DissolveScript>().StartDissolve();
    }

    void OnTriggerExit(Collider coll)
    {
        inTrigger = false;
    }

    void FlipLights(bool input)
    {
        plateLightOn.SetActive(input);
        plateLightOff.SetActive(!input);
    }
}