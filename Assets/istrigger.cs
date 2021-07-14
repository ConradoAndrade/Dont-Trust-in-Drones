using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class istrigger : MonoBehaviour
{

    public GameObject enemy;

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col != null)
        {

            if (col.gameObject.CompareTag("Player"))
            {


                enemy.GetComponent<enemy_ai>().unlish = true;

            }
        }


    }

}
