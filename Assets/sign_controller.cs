using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TriggedSignEV : UnityEvent<string> { }

public class sign_controller : MonoBehaviour
{
    public BoxCollider2D trigger;
    public bool isInsign;
    public TriggedSignEV TriggedSignEV;
    public AudioSource quote;

    [SerializeField] GameObject showAction;

    [SerializeField] GameObject cam;

    public string heroResponse;

    void Start()
    {

    }

    void Update()
    {

        if (Input.GetKey(KeyCode.E) && isInsign)
        {
            TriggedSignEV.Invoke(heroResponse);
            isInsign = false;
            showAction.SetActive(false);
            trigger.enabled = false;
            showAction.SetActive(false);

            if (quote.clip != null)
            {
                quote.Play(0);
            }
        }
    }


    public void backAlone(float val)
    {
        Invoke("bye", val);
    }

    void bye()
    {
        cam.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            showAction.SetActive(true);
            isInsign = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            showAction.SetActive(true);
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
 
        isInsign = false;
        showAction.SetActive(false);

    }
}
