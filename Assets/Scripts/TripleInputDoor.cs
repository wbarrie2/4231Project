using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleInputDoor : MonoBehaviour
{
    public GameObject doorClosingBox;
    public Vector3 moveDoorTo;
    public float doorMoveSpeed;
    Vector3 doorStart;
    DoorCheck doorCheck;
    Vector3 applyForce;
    //Door Variables

    public int activationScale;
    public GameObject SunTrigger;
    public GameObject CrossTrigger;
    public GameObject ArrowTrigger;
    float activated = 0;
    //Trigger Variables

    void Start()
    {
        doorStart = transform.position;
        doorCheck = doorClosingBox.GetComponent<DoorCheck>();
        applyForce = new Vector3(moveDoorTo.x - doorStart.x, moveDoorTo.y - doorStart.y, moveDoorTo.z - doorStart.z);
        AdjustForce();
    }

    void Update()
    {
        if (CheckTriggers())
        {
            if (activated == 0)
            {
                activated = 10;
            }
            else if (activated < 5)
            {
                activated = 5;
            }
        }
        else
        {
            activated = 0;
        }

        //if (activated >= 5)
        {
            //if (transform.position.z > moveDoorTo && doorCheck.detection == 1)
            {
                //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - doorMoveSpeed * Time.deltaTime);
            }
        }
        //else
        {
           // if (transform.position.z < originalZ && doorCheck.detection == 1)
            {
               // transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + doorMoveSpeed * Time.deltaTime);
            }
        }

        if (activated >= 5)
        {
            if (transform.position != moveDoorTo)
            {
                transform.Translate(applyForce * doorMoveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, moveDoorTo) < 0.1)
                {
                    transform.position = moveDoorTo;
                }
            }
        }
        else if (doorCheck.detection == 1)
        {
            if (transform.position != doorStart)
            {
                transform.Translate(applyForce * -doorMoveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, doorStart) < 0.1)
                {
                    transform.position = doorStart;
                }
            }
        }

        if (activated > 0)
        {
            activated -= activationScale * Time.deltaTime * doorCheck.detection;
        }
        else if (activated < 0)
        {
            activated = 0;
        }
    }

    bool CheckTriggers()
    {
        return
            (
                SunTrigger.GetComponent<BlockTriggerZone>().InTrigger() &&
                CrossTrigger.GetComponent<BlockTriggerZone>().InTrigger() &&
                ArrowTrigger.GetComponent<BlockTriggerZone>().InTrigger()
            );
    }

    void AdjustForce()
    {
        float newX = applyForce.x == 0 ? 0 : Mathf.Sign(applyForce.x);
        float newY = applyForce.y == 0 ? 0 : Mathf.Sign(applyForce.y);
        float newZ = applyForce.z == 0 ? 0 : Mathf.Sign(applyForce.z);
        applyForce = new Vector3(newX, newY, newZ);
    }
}