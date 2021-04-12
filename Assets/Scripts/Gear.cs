using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public float speed = 55;
    public int importance = 0;
    public bool invert = false;
    Transform gear;

    void Start()
    {
        gear = transform.parent.gameObject.transform;
    }

    void Update()
    {
        if (invert)
        {
            gear.transform.Rotate(new Vector3(0, 0, speed) * (-Time.timeScale + 1) * Time.unscaledDeltaTime);
        }
        else
        {
            gear.transform.Rotate(new Vector3(0, 0, speed) * Time.timeScale * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (invert)
        {
            if (other.transform.parent != null)
            {
                if (other.transform.parent.GetComponent<Gear>().importance < importance)
                {
                    other.transform.parent = this.transform;
                    other.transform.localScale = new Vector3(.004f, .008f, .008f);
                }
            }
            else
            {
                other.transform.parent = this.transform;
                other.transform.localScale = new Vector3(.004f, .008f, .008f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (invert)
        {
            if (other.transform.parent != null)
            {
                if (other.transform.parent.GetComponent<Gear>().importance == importance)
                {
                    other.transform.parent = null;
                    other.transform.localScale = new Vector3(2, 2, 2);
                }
            }
        }
    }
}