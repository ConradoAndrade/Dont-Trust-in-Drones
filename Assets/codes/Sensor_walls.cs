using UnityEngine;
using System.Collections;


public class Sensor_walls : MonoBehaviour {



    public bool state;
    public string tag;

    private void OnEnable()
    {

    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            state = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            state = false;
        }
    }

    void Update()
    {

    }


}
