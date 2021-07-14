using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leavegame : MonoBehaviour
{

    public GameObject ui;


    void Start()
    {
        
    }

    public void Update()
    {

        if (general.Instance.obj21 && general.Instance.obj22 && general.Instance.obj23)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ui.SetActive(true);
            }
        }
    }
}
