using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class enemy_controller : MonoBehaviour
{
    public AIPath[] aipath;

    //public GameObject[] Enemys;

    void Start()
    {
        deactivateEnemys();
    }


    void Update()
    {
        
    }

    public void deactivateEnemys()
    {
        foreach (var enemy in aipath)
        {
            enemy.canSearch = false;

        }
    }

    public void activateDrones()
    {
        foreach (var enemy in aipath)
        {
            enemy.canSearch = true;

        }
    }
}
