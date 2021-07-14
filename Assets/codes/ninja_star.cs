using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninja_star : MonoBehaviour
{
    public GameObject hitParticles;
    public float timeToDestroy = 0.4f;
    public Animator anim;

    public int starDamage = 15;

    public AudioSource hitSound;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col != null) { 
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = col.gameObject.GetComponent<Rigidbody2D>();
            hitParticles.SetActive(true);
            anim.SetTrigger("hit");

            if (hitSound.clip != null)
            {
                hitSound.Play(0);
            }

            Destroy(gameObject, timeToDestroy);

            if (col.gameObject.CompareTag("enemy") || col.gameObject.CompareTag("enemyStay"))
            {
                col.gameObject.GetComponent<enemy_status>().Damage(starDamage);
            }
            else if (col.gameObject.CompareTag("server"))
            {
                Debug.Log("HITTED SERVER");
                col.gameObject.GetComponent<server_status>().Damage(starDamage - 5);
            }
            else if (col.gameObject.CompareTag("lever"))
            {
                Debug.Log("HITTED SERVER");
                col.gameObject.GetComponent<lever_controller?>().fireLever();
         
            }
        }

    }
}
