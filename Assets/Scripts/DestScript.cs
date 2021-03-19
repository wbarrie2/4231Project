using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestScript : MonoBehaviour
{
    public bool inWall = false;
    public LayerMask groundMask;
    public Transform physicsBox;
    private Vector3 boxSize = new Vector3(.75f, .75f, 3);

    void Update()
    {
        inWall = Physics.CheckBox(physicsBox.position, boxSize, physicsBox.rotation, groundMask);
    }
}