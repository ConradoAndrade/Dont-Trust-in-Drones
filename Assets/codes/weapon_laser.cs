using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_laser : MonoBehaviour
{
    Ray2D ray;

    public LineRenderer lineRenderer;
    public Transform LaserShoot;
    public Transform LaserHit;

    private Vector2 playerPos;

    public float offsetLaser;

    public LayerMask mask;

    public GameObject target;
    public GameObject hitParticle;


    public GameObject followingMe; 
    public int followDistance;
    private List<Vector3> storedPositions;

    public bool shoot;
    public int powerShot;


    void Start()
    {
        storedPositions = new List<Vector3>();

        lineRenderer.useWorldSpace = true;

        LaserHit = GameObject.Find("NinjaHero").transform;
    }

    void FixedUpdate()
    {
        if (shoot) {
            raycast_travel();
        }
        else
        {
            RemoveLaser();
        }
    }

    public void raycast_travel()
    {

        playerPos = new Vector2(LaserHit.position.x, LaserHit.position.y);
        playerPos = new Vector2(LaserHit.position.x - LaserShoot.position.x, LaserHit.position.y - LaserShoot.position.y);

        storedPositions.Add(playerPos);

        if (storedPositions.Count > followDistance)
        {
            //followingMe.transform.position = storedPositions[0];

            ray = new Ray2D(LaserShoot.position, storedPositions[0]);
            RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction + new Vector2(0, 0.2f), 100f, mask);

            //Debug.DrawRay(ray.origin,ray.direction,Color.blue);

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
                    }
                    else
                    {

                    }
                }
                else
                {
                    //Debug.Log("Detected other objects");
                }

         

            }
            else
            {
                //Debug.Log("No collisions with any objects");
            }

            storedPositions.RemoveAt(0);


        }

    }

    private void RemoveLaser()
    {
        lineRenderer.SetPosition(0, Vector2.zero);
        lineRenderer.SetPosition(1, Vector2.zero);
        hitParticle.SetActive(false);

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
