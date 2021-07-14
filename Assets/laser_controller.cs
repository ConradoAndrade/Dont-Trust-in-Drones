using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser_controller : MonoBehaviour
{
    Ray2D ray;

    [SerializeField] Renderer laserBodyRend;

    public LineRenderer lineRenderer;
    public Transform LaserShoot;
    public Transform LaserHitGO;
    public Transform LaserHit;

    public float offsetLaser;

    public LayerMask mask;

    public GameObject componentsLaser;
    public GameObject smoke;
    public GameObject hitParticle;

    public bool shoot;
    public int powerShot;

    void Start()
    {
        lineRenderer.useWorldSpace = true;
        LaserHit = LaserHitGO.transform;
    }

    void FixedUpdate()
    {
        if (shoot)
        {
            raycast_travel();
        }
        
    }

    public void raycast_travel()
    {
        ray = new Ray2D(LaserShoot.position, LaserHit.position);
        RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction, 100f, mask);

        lineRenderer.SetPosition(0, LaserShoot.position);
        lineRenderer.SetPosition(1, info.point);

        hitParticle.transform.position = info.point;
        hitParticle.SetActive(true);

        if (info.collider != null)
        {
            //Debug.Log("RayCast: " + info.transform.gameObject);

            if (info.transform.gameObject.CompareTag("Player"))
            {
                //Debug.LogWarning("Detected Enemies");

                if (Random.Range(-50f, 100f) > 70f)
                {
                    general.Instance.DamageHero(powerShot);
                    LaserHit.GetComponent<hero_controller>().dano(general.Instance.lifeHero);

                    Debug.Log("acertou laser");

                }
                else
                {

                }
            }
            else
            {

            }

        }

    }

    public void RemoveLaser()
    {
        lineRenderer.SetPosition(0, Vector2.zero);
        lineRenderer.SetPosition(1, Vector2.zero);
        hitParticle.SetActive(false);
        laserBodyRend.material.SetFloat("_deactivated", 1);
        smoke.SetActive(true);
        componentsLaser.SetActive(false);
        shoot = false;
    }

    public void shootState(bool state)
    {
        if (state)
        {
            shoot = true;
        }
        else
        {
            shoot = false;
        }
    }



}
