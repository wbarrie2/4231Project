using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public int desiredFPS = 60;

    void Awake()
    {
        Application.targetFrameRate = desiredFPS;
        QualitySettings.vSyncCount = 0;
    }
}
