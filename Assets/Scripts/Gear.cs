using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public enum typeList { normal, invert, flipFlop };
    public typeList gearType = typeList.normal;
    public float speed = 55;
    public int importance = 0;
    Transform gear;

    void Start()
    {
        gear = transform.parent.gameObject.transform;
    }

    void Update()
    {
        switch (gearType)
        {
            case (typeList.invert):
                {
                    gear.transform.Rotate(new Vector3(0, 0, speed) * (-Time.timeScale + 1) * Time.unscaledDeltaTime);
                    break;
                }
            case (typeList.flipFlop):
                {
                    if (Time.timeScale > .5)
                    {
                        gear.transform.Rotate(new Vector3(0, 0, speed) * Time.timeScale * Time.unscaledDeltaTime);
                    }
                    else
                    {
                        gear.transform.Rotate(new Vector3(0, 0, -speed) * (-Time.timeScale + 1) * Time.unscaledDeltaTime);
                    }
                    break;
                }
            case (typeList.normal): 
            default:
                {
                    gear.transform.Rotate(new Vector3(0, 0, speed) * Time.timeScale * Time.deltaTime);
                    break;
                }
        }
    }

    private void OnTriggerEnter(Collider other)
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

    private void OnTriggerExit(Collider other)
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