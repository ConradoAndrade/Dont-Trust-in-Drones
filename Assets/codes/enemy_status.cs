using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class enemyDied : UnityEvent<bool> { }

public class enemy_status : MonoBehaviour
{
    public enemyDied enemyDied;

    public int life = 100;
    [SerializeField] int curState;
    public Animator anim;

    public enemy_ai ai;
    public SpringJoint2D holder;

    public bool isBoss;

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
            anim.SetInteger("droneState", 3);
            ai.dead = true;
            enemyDied.Invoke(true);
            if (isBoss)
            {
                holder.enabled = false;
            }
        }
    }

    public void states(int val)
    {

        if (val > 70 && val < 100)
        {
            curState = 0;
        }
        else if (val > 40 && val < 69)
        {
            curState = 1;
        }
        else if (val > 20 && val < 39)
        {
            curState = 2;
        }
        else if (val > 1 && val < 19)
        {
            curState = 3;
        }

        switch (curState)
        {
            case 1:
                anim.SetInteger("droneState", 0);
                break;
            case 2:
                anim.SetInteger("droneState", 1);
                break;
            case 3:
                anim.SetInteger("droneState", 2);
                break;
            default:
                anim.SetInteger("droneState", 0);
                break;
        }


    }
}
