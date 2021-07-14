using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potion_controller : MonoBehaviour
{
    public GameObject text;
    public GameObject hitParticles;
    public float timeToDestroy = 0.1f;
    public SpriteRenderer potionGFX;
    public Rigidbody2D rb;

    public GameObject player;
    public float timeToDesapear;

    public AudioSource grabSound;


    //MODIFICAR O TEXTO PARA QUE DIGA O VALOR EXATO DO PODER DA POTION - TEXT MESH PRO
    public int potionPower = 30;

    public int potionType = 0;

    private void Start()
    {
        player = GameObject.Find("NinjaHero");
    }

    public void blowup()
    {
        hitParticles.SetActive(true);
        text.SetActive(true);
        Destroy(gameObject, timeToDesapear);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col != null)
        {

            if (col.gameObject.CompareTag("Player"))
            {
                rb.gravityScale = -0.7f;
                rb.simulated = false;

                if (grabSound.clip != null)
                {
                    grabSound.Play(0);
                }

                //col.gameObject.GetComponent<enemy_status>().Damage(starDamage);
                StartCoroutine("Fade");
                Invoke("blowup", 0.5f);

                if (potionType == 1)
                {
                    general.Instance.lifeHero += potionPower;
                }
                else if (potionType == 2)
                {
                    general.Instance.staminaHero += potionPower;
                }
                else if (potionType == 3)
                {
                    general.Instance.globalHeroStars += potionPower;
                }

                player.GetComponent<hero_controller>().heroExpressions(1);

            }
        }

    }

    IEnumerator Fade()
    {
        Color newColor = potionGFX.color;
        for (float f = 1f; f >= 0; f -= 0.5f)
        {
            newColor.a = f;
            potionGFX.color = newColor;
            yield return new WaitForSeconds(0.4f);
        }
    }

}
