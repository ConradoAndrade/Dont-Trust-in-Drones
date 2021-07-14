using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class de_active_onCall : MonoBehaviour
{

    public void active()
    {
        gameObject.SetActive(true);
    }

    public void deactive()
    {
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
