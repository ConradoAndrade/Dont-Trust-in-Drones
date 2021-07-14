using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal_controller : MonoBehaviour
{

    public BoxCollider2D col;
    public BoxCollider2D colBlock;

    public bool isReady;

    public SpriteRenderer playerGFX;

    public GameObject player;
    public Sprite portalIMG;

    public GameObject sound;
    public GameObject particles;
    public float thrust = 1f;

    public GameObject leavegame;



    public CanvasGroup CV;
    void Start()
    {
        
    }

    void Update()
    {
        if (general.Instance.obj21 && general.Instance.obj22 && general.Instance.obj23)
        {
            isReady = true;
        }
    }

    void fadeend()
    {
        StartCoroutine("FadeEnd");
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col != null)
        {

            if (col.gameObject.CompareTag("Player") )
            {

                Debug.Log(col.gameObject.tag);

                if (isReady)
                {
                    Time.timeScale = 0.3f;

                    colBlock.enabled = false;
                    player.GetComponent<Animator>().enabled = false;
                    player.GetComponent<PrototypeHeroDemo>().enabled = false;
                    playerGFX.sprite = portalIMG;
                    StartCoroutine("Fade");
                    particles.SetActive(true);

                    Invoke("fadeend", 1f);
                    SetAudioMute(true);
                    leavegame.SetActive(true);

                }
                else
                {
                    player.GetComponent<hero_controller>().prepareDialog("I'm not worthy");
                }


            }
        }


    }


    void SetAudioMute(bool mute)
    {
        AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int index = 0; index < sources.Length; ++index)
        {
            sources[index].mute = mute;
        }
       
    }


    IEnumerator Fade()
    {
        Color newColor = playerGFX.color;
        for (float f = 1f; f >= -1; f -= 0.1f)
        {
            newColor.a = f;
            playerGFX.color = newColor;
            player.GetComponent<Rigidbody2D>().AddForce(transform.up * thrust * f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeEnd()
    {
        sound.SetActive(true);
        for (float f = 0f; f <= 2; f += 0.06f)
        {
            CV.alpha = f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
