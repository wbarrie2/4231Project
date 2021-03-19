using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveScript : MonoBehaviour {

    Material mat;
    bool dissolving = false;
    float amount = 0;
    public Transform spawnPoint;

    void Start() {
        mat = GetComponent<Renderer>().material;
    }

    void Update() {

        if (dissolving)
        {
            if (amount > .9)
            {
                dissolving = false;
                amount = 0;
                GetComponent<PickUp>().grabbed = false;
                transform.position = spawnPoint.position;
            }
            else
            {
                amount += Time.deltaTime / 2;
            }
            mat.SetFloat("_DissolveAmount", amount);
        }
    }

    public void StartDissolve()
    {
        dissolving = true;
    }
}