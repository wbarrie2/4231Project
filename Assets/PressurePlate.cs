using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject doorClosingBox;
    public GameObject doorLightOn;
    public GameObject doorLightOff;
    public Transform door;
    public float doorMoveSpeed;
    public Vector3 moveDoorTo;
    Vector3 doorStart;
    Vector3 applyForce;
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
        doorStart = door.position;
        originalY = transform.position.y;
        doorCheck = doorClosingBox.GetComponent<DoorCheck>();
        applyForce = new Vector3(moveDoorTo.x - doorStart.x, moveDoorTo.y - doorStart.y, moveDoorTo.z - doorStart.z);
        AdjustForce();
    }

    void Update()
    {
        if (activated >= 5)
        {
            if (door.position != moveDoorTo)
            {
                door.Translate(applyForce * doorMoveSpeed * Time.deltaTime);

                if (Vector3.Distance(door.position, moveDoorTo) < 0.1)
                {
                    door.position = moveDoorTo;
                }
            }
        } 
        else if (doorCheck.detection == 1)
        {
            if (door.position != doorStart)
            {
                door.Translate(applyForce * -doorMoveSpeed * Time.deltaTime);
                if (Vector3.Distance(door.position, doorStart) < 0.1)
                {
                    door.position = doorStart;
                }
            }
            if (transform.position.y < originalY)
            {
                transform.Translate(Vector3.up * plateMoveSpeed * Time.deltaTime);
            }
        }

        if (activated > 0)
        {
            activated -= activationScale * Time.deltaTime * doorCheck.detection;
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
        activated = 10;
        transform.position = new Vector3(transform.position.x, movePlateTo, transform.position.z);
        inTrigger = true;
        FlipLights(true);
    }

    void OnTriggerExit(Collider coll)
    {
        inTrigger = false;
    }

    void FlipLights(bool input)
    {
        plateLightOn.SetActive(input);
        plateLightOff.SetActive(!input);

        doorLightOn.SetActive(input);
        doorLightOff.SetActive(!input);
    }

    void AdjustForce()
    {
        float newX = applyForce.x == 0 ? 0 : Mathf.Sign(applyForce.x);
        float newY = applyForce.y == 0 ? 0 : Mathf.Sign(applyForce.y);
        float newZ = applyForce.z == 0 ? 0 : Mathf.Sign(applyForce.z);
        applyForce = new Vector3(newX, newY, newZ);
    }
}