using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveScript : MonoBehaviour {

    Material mat;
    bool dissolving = false;
    float amount = 0;
    public Transform spawnPoint;
    public bool randomColor = false;
    public bool canBeGrabbed = true;

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
                if (canBeGrabbed)
                {
                    GetComponent<PickUp>().grabbed = false;
                }
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
        if (randomColor)
        {
            mat.SetColor("_DissolveColor", new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f)));
        }
        dissolving = true;
    }
}