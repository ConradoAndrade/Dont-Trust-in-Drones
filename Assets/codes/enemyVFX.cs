using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyVFX : MonoBehaviour
{

    public AIPath aipath;


    void Update()
    {
        if (aipath.desiredVelocity.x >= 0.01f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (aipath.desiredVelocity.x <= 0.01f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
