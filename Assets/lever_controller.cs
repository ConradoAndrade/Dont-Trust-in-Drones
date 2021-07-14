using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TriggedLever : UnityEvent<bool> { }

public class lever_controller : MonoBehaviour
{

    public TriggedLever TriggedLever;
    public Animator anim;
    public bool fired;

    public AudioSource leverSound;



    public void fireLever()
    {
        TriggedLever.Invoke(true);
        anim.SetBool("ativated", true);
        fired = true;

        if (leverSound.clip != null)
        {
            leverSound.Play(0);
        }

        general.Instance.obj1 = true;
    }
}
