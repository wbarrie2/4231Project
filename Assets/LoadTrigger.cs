using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTrigger : MonoBehaviour
{
    public string level;

    void OnTriggerEnter(Collider coll)
    {
        SceneManager.LoadScene(level);
    }
}