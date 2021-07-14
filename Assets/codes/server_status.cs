using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[System.Serializable]
public class serverDied : UnityEvent<int> { }

public class server_status : MonoBehaviour
{

    public serverDied serverDied;

    public int life = 100;
    [SerializeField] int curState;
    public Animator anim;

    public AudioSource exploSound;


    void Start()
    {

    }

    void Update()
    {

    }

    public void Damage(int val)
    {

        if (life >= 1)
        {
            life -= val;
            states(life);
        }
        else if (life <= 0)
        {
            life = 0;
            anim.SetInteger("damage", 3);
            serverDied.Invoke(0);

            if (exploSound.clip != null)
            {
                exploSound.Play(0);
            }
        }
    }

    public void states(int val)
    {

        if (val > 50 && val < 100)
        {
            curState = 1;
        }
        else if (val > 1 && val < 49)
        {
            curState = 2;
        }

        switch (curState)
        {
            case 1:
                anim.SetInteger("damage", 1);
                break;
            case 2:
                anim.SetInteger("damage", 2);
                break;
            default:
                anim.SetInteger("damage", 0);
                break;
        }


    }
}
