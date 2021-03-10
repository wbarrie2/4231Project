using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject doorClosingBox;
    public GameObject doorLightOn;
    public GameObject doorLightOff;
    public Transform door;
    public float moveDoorTo;
    public float doorMoveSpeed;
    float originalX;
    DoorCheck doorCheck;
    //Door Variables

    public GameObject plateLightOn;
    public GameObject plateLightOff;
    public float movePlateTo;
    public float plateMoveSpeed;
    public int activationScale;
    float activated = 0;
    float originalY;
    bool inTrigger;
    //Plate Variables
    
    void Start()
    {
        originalX = door.position.x;
        originalY = transform.position.y;
        doorCheck = doorClosingBox.GetComponent<DoorCheck>();
    }

    void Update()
    {
        if (activated >= 5)
        {
            if (door.position.x > moveDoorTo && doorCheck.detection == 1)
            {
                door.position = new Vector3(door.position.x - doorMoveSpeed * Time.deltaTime, door.position.y, door.position.z);
            }
        } else
        {
            if (door.position.x < originalX && doorCheck.detection == 1)
            {
                door.position = new Vector3(door.position.x + doorMoveSpeed * Time.deltaTime, door.position.y, door.position.z);
            }
            if (transform.position.y < originalY && doorCheck.detection == 1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + plateMoveSpeed * Time.deltaTime, transform.position.z);
            }
        }

        if (activated > 0)
        {
            activated -= activationScale * Time.deltaTime * doorCheck.detection;
        } else if (activated < 0)
        {
            activated = 0;
            flipLights(false);
            door.position = new Vector3(originalX, door.position.y, door.position.z);
            transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
        }

        if (inTrigger && activated < 5)
        {
            activated = 5;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        activated = 10;
        transform.position = new Vector3(transform.position.x, movePlateTo, transform.position.z);
        inTrigger = true;
        flipLights(true);
    }

    void OnTriggerExit(Collider coll)
    {
        inTrigger = false;
    }

    void flipLights(bool input)
    {
        plateLightOn.SetActive(input);
        plateLightOff.SetActive(!input);

        doorLightOn.SetActive(input);
        doorLightOff.SetActive(!input);
    }
}