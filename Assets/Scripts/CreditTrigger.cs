using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditTrigger : MonoBehaviour
{
    public float moveTo;
    public int moveSpeed;
    bool hasRun = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasRun)
        {
            Credits temp;
            foreach (Transform child in transform)
            {
                temp = child.gameObject.GetComponent<Credits>();
                if (temp != null)
                {
                    temp.StartCredits(moveTo, moveSpeed);
                }
            }
            hasRun = true;
        } 
    }
}