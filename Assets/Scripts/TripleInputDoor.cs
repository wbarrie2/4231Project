using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleInputDoor : MonoBehaviour
{
    public GameObject doorClosingBox;
    public float moveDoorTo;
    public float doorMoveSpeed;
    float originalZ;
    DoorCheck doorCheck;
    //Door Variables

    public int activationScale;
    public GameObject SunTrigger;
    public GameObject CrossTrigger;
    public GameObject ArrowTrigger;
    float activated = 0;
    //Trigger Variables

    void Start()
    {
        originalZ = transform.position.z;
        doorCheck = doorClosingBox.GetComponent<DoorCheck>();
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

        if (activated >= 5)
        {
            if (transform.position.z > moveDoorTo && doorCheck.detection == 1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - doorMoveSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (transform.position.z < originalZ && doorCheck.detection == 1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + doorMoveSpeed * Time.deltaTime);
            }
        }

        if (activated > 0)
        {
            activated -= activationScale * Time.deltaTime * doorCheck.detection;
        }
        else if (activated < 0)
        {
            activated = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y, originalZ);
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
}