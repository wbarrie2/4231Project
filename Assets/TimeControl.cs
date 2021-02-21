using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Time.timeScale = (Time.timeScale * -1) + 1;
        }
    }
}