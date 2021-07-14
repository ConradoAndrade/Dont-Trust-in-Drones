using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest_controller : MonoBehaviour
{
    public GameObject hitParticles;

    public GameObject player;
    public GameObject respawItemOrig;
    public GameObject respawItemPrefab;

    public BoxCollider2D collider;

    public AudioSource grabSound;

    public Animator anim;


    private void Start()
    {
        player = GameObject.Find("NinjaHero");
    }

    public void blowup()
    {
        hitParticles.SetActive(true);
        GameObject item = Instantiate(respawItemPrefab, respawItemOrig.transform.position, Quaternion.identity) as GameObject;

        collider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col != null)
        {

            if (col.gameObject.CompareTag("Player"))
            {

                if (grabSound.clip != null)
                {
                    grabSound.Play(0);
                }
                anim.SetBool("open", true);


                Invoke("blowup", 1f);

            }
        }

    }


}
