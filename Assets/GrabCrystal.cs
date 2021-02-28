using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabCrystal : MonoBehaviour
{
    public Text pickUpText;
    public GameObject player;
    bool inTrigger = false;

    void Start()
    {
        pickUpText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (inTrigger && Input.GetMouseButtonDown(0))
        {
            player.GetComponent<TimeControl>().enabled = true;
            pickUpText.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        pickUpText.gameObject.SetActive(true);
        inTrigger = true;
    }
    void OnTriggerExit(Collider coll)
    {
        pickUpText.gameObject.SetActive(false);
        inTrigger = false;
    }
}