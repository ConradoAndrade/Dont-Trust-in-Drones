using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class hero_controller : MonoBehaviour
{
    [SerializeField] GameObject heroFaceGO;
    Image m_Image;
    [SerializeField] Sprite[] faces;

    [SerializeField] TextMeshProUGUI lifeUITex;
    [SerializeField] TextMeshProUGUI staminaUITex; 
    [SerializeField] TextMeshProUGUI starsUITex; 

    [SerializeField] TextMeshProUGUI deatsUITex;
    [SerializeField] TextMeshProUGUI obj1UITex;
    [SerializeField] TextMeshProUGUI obj2UITex;
    [SerializeField] TextMeshProUGUI obj3UITex;

    [SerializeField] int faceDamage;
    [SerializeField] int faceState;

    [SerializeField] GameObject trhowHand;
    [SerializeField] GameObject ninjaStar;

    [SerializeField] SpriteRenderer heroSprite;
    [SerializeField] Animator m_animator;


    [SerializeField] bool changeFace;

    [SerializeField] GameObject orgPostProcessing;
    [SerializeField] GameObject dialogCam;
    [SerializeField] GameObject baloon;
    [SerializeField] TextMeshPro baloonTex;

    [SerializeField] GameObject respawn;

    public AudioSource trhowSound;
    public AudioSource dieSound;

    [SerializeField] GameObject respawnItens;
    [SerializeField] GameObject respawnItensPrefab;

    bool isDialogReady;


    public Rigidbody2D rb;
    public int clickForce = 500;

    public Vector2 trhowHandoffset;

    public Vector3 worldPosition;

    void Start()
    {
        m_Image = heroFaceGO.GetComponent<Image>();
    }

    void Update()
    {
        int _lifeNow = general.Instance.lifeHero;
        int _staminaNow = general.Instance.staminaHero;
        int _starsNow = general.Instance.globalHeroStars;
        int _deathsNow = general.Instance.globalHeroDeats;

        deatsUITex.text = "DEATHS- 000" + _deathsNow;

        if (general.Instance.obj21)
        {
            obj1UITex.color = new Color32(51, 156, 29, 188);
        }
        if (general.Instance.obj22)
        {
            obj2UITex.color = new Color32(51, 156, 29, 188);
        }
        if (general.Instance.obj23)
        {
            obj3UITex.color = new Color32(51, 156, 29, 188);
        }




        if (_lifeNow >= 1)
        {
            lifeUITex.text = "LIFE: " + _lifeNow;

        }
        else
        {
            lifeUITex.text = "MORREU -> " + _lifeNow;

        }

        if (_staminaNow >= 1)
        {
            staminaUITex.text = "STAMINA: " + _staminaNow;
        }
        else
        {
            staminaUITex.text = "NO STAMINA";
        }

        if (_starsNow >= 1)
        {
            starsUITex.text = "NINJA STARS: " + _starsNow;
        }
        else
        {
            starsUITex.text = "NO STARS";
        }

        if (general.Instance.staminaHero < 100)
        {
            InvokeRepeating("reloadStamina", 4f, 10f);
        }



        if (Input.GetMouseButtonDown(0))
        {
            if (general.Instance.globalHeroStars >= 1)
            {
                general.Instance.globalHeroStars --;

                trhowSound.Play(0);

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = (mousePos - trhowHand.transform.position);

                if (direction.x >= 0.01f)
                {
                    heroSprite.flipX = false;
                }
                else if (direction.x <= -0.01f)
                {
                    heroSprite.flipX = true;
                }

                //m_animator.SetTrigger("throw");

                var bullet = Instantiate(ninjaStar);

                bullet.transform.position = new Vector2(trhowHand.transform.position.x + trhowHandoffset.x, trhowHand.transform.position.y - trhowHandoffset.y);
                bullet.transform.rotation = trhowHand.transform.rotation;
                Rigidbody2D rigidBody = bullet.GetComponent<Rigidbody2D>();
                rigidBody.AddForceAtPosition(direction.normalized * clickForce, trhowHand.transform.position, ForceMode2D.Impulse);
            }
        }

        
        if (isDialogReady)
        {
            Invoke("removeDialog", 3f);
            isDialogReady = false ;

        }

        if (_lifeNow <= 0)
        {
            die();
            if (dieSound.clip != null)
            {
                dieSound.Play(0);
            }

            Instantiate(respawnItensPrefab, new Vector2(respawnItens.transform.position.x, respawnItens.transform.position.y), Quaternion.identity);

            var clones = GameObject.FindGameObjectsWithTag("enemy");
            foreach (var clone in clones)
            {
                Destroy(clone);
            }
        }

    }


    public void reloadStamina()
    {
        general.Instance.staminaHero += 1;
        CancelInvoke("reloadStamina");
        
    }

    public void dano(int val)
    {

        heroFaces(val, true);
    }

    public void die()
    {
        gameObject.transform.position = respawn.transform.position;
        general.Instance.ResetHero();
        general.Instance.globalHeroDeats++;

    }

    public void prepareDialog(string text)
    {
        //orgPostProcessing.SetActive(false);
        Debug.Log("preparedialog");

        dialogCam.SetActive(true);
        baloon.SetActive(true);
        baloonTex.text = text;
        Invoke("showDialog", 0.5f);
    }

    public void showDialog()
    {
        Debug.Log("showDialog");
        baloon.SetActive(true);
        isDialogReady = true;
    }

    public void removeDialog()
    {
        Debug.Log("removeDialog");

        baloon.SetActive(false);
        //orgPostProcessing.SetActive(true);
        dialogCam.SetActive(false);
        //CancelInvoke();
            
    }

    public void heroExpressions(int val)
    {
        heroFaces(val, false);
        Invoke("heroResetExpression", 3f);
    }

    public void heroResetExpression()
    {
        if (general.Instance.globalHeroFace >= 4)
        {
            heroFaces(general.Instance.globalHeroFace, false);
        }

        heroFaces(3, false);
        //CancelInvoke();
    }

    public void registerObj(int val)
    {
        if (val == 1)
        {
            general.Instance.obj1 = true;

        }
        else if (val == 2)
        {
            general.Instance.obj2 = true;

        }
        else if (val == 3)
        {
            general.Instance.obj3 = true;

        }
    }

    public void registerObj2(int val)
    {
        if (val == 1)
        {
            general.Instance.obj21 = true;

        }
        else if (val == 2)
        {
            general.Instance.obj22 = true;

        }
        else if (val == 3)
        {
            general.Instance.obj23 = true;

        }
    }


    public void heroFaces(int val, bool isDamage)
    {

        if (val > 80 && val < 100 )
        {
            faceState = 3;
        }
        else if (val > 50 && val < 79)
        {
            faceState = 4;
        }
        else if (val > 1 && val < 49)
        {
            faceState = 5;
        }

        //Debug.Log(faceState);

        if (!isDamage)
        {
            faceState = val;
        }

        switch (faceState)
        {
            case 1:
                m_Image.sprite = faces[0];
                break;
            case 2:
                m_Image.sprite = faces[1];
                break;
            case 3:
                m_Image.sprite = faces[2];
                break;
            case 4:
                m_Image.sprite = faces[3];
                break;
            case 5:
                m_Image.sprite = faces[4];
                break;
            case 6:
                m_Image.sprite = faces[5];
                break;
            default:
                break;
        }



    }

}
