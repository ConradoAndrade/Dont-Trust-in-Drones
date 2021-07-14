using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemy_ai : MonoBehaviour
{

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float endReachedDistance = 3f;
    public float ShootLoadTime = 3f;

    public bool unlish;

    public weapon_laser weapon;

    public SpriteRenderer enemyGFX;

    Path path;

    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    public bool dead;

    private IEnumerator coroutine;

    private bool isCoroutineExecuting = false;

    public AudioSource soundDrone;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("NinjaHero").transform;



    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }

    }

    public void StartAttak()
    {
        Invoke("follow", 1f);
        StartCoroutine("Fade");
        unfreeze();

    }

    void follow()
    {
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    public void Unlishthemonster()
    {
        unlish = true;
        
    }

    void unfreeze()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation ;

    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void shootRoutine()
    {
        coroutine = ShootTimer(ShootLoadTime);
        StartCoroutine(coroutine);
    }

    void FixedUpdate()
    {
        if (unlish)
        {
            StartAttak();
            unlish = false;
        }

        if (dead)
        {
            weapon.shootState(false);
            CancelInvoke();
 
            rb.gravityScale = 2f;
            rb.freezeRotation = false;
            soundDrone.mute = true;
            soundDrone.Stop();

            return;

        }

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count - endReachedDistance)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        if (path.vectorPath.Count - endReachedDistance <= 3)
        {
         
            InvokeRepeating("shootRoutine", 0f, ShootLoadTime);
            
        }





        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);




        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }


        Debug.Log(distance);

        if (distance <= 2)
        {
            soundDrone.Play(0);
        }



        if (force.x >= 0.01f)
        {
            enemyGFX.flipX = false;

        }
        else if (force.x <= -0.01f)
        {
            enemyGFX.flipX = true;
        }


    }


    public IEnumerator ShootTimer(float time)
    {
        weapon.shootState(true);

        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        weapon.shootState(false);

        isCoroutineExecuting = false;
    }

    IEnumerator Fade()
    {
        Color newColor = enemyGFX.color;
        for (float f = 0f; f <= 1; f += 0.1f)
        {
            newColor.a = f;
            enemyGFX.color = newColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
