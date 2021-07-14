using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[System.Serializable]
public class TriggedAlarmEV : UnityEvent<int, bool> { }

public class alarm_video_camera : MonoBehaviour
{

    [SerializeField] Renderer rend;

    [SerializeField] GameObject light_green;
    [SerializeField] GameObject light_red;
    [SerializeField] float light_pulse;
    [SerializeField] bool fired;
    [SerializeField] bool died;

    [SerializeField] GameObject camera;


    [SerializeField] CircleCollider2D  cameraFieldOfView;
    [SerializeField] GameObject startViewPoint;
    [SerializeField] GameObject finishViewPoint;

    [SerializeField] GameObject spawnSpot;
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] float spawnTime;

    public AudioSource alarm;


    [SerializeField] float PULSE_RANGE = 4.0f;
    [SerializeField] float PULSE_SPEED = 3.0f;

    [SerializeField] float PULSE_MINIMUM = 1.0f;

    public GameObject player;

    public TriggedAlarmEV TriggedAlarmEV;

    private bool dirRight = true;
    public float speed = 2.0f;

    void Start()
    {
        rend.material.SetFloat("_state", 0);
        light_red.SetActive(false);
        light_green.SetActive(true);
        player = GameObject.Find("NinjaHero");


    }

    void Update()
    {
        light_red.transform.GetComponent<Light2D>().intensity = light_pulse;


        light_pulse = PULSE_MINIMUM +
                          Mathf.PingPong(Time.time * PULSE_SPEED,
                                         PULSE_RANGE - PULSE_MINIMUM);

        //transform.position = Vector2.Lerp(startViewPoint.transform.position, finishViewPoint.transform.position, Mathf.PingPong(Time.time * speed, 1.0f));
        cameraFieldOfView.offset = Vector2.Lerp(startViewPoint.transform.localPosition, finishViewPoint.transform.localPosition, Mathf.PingPong(Time.time * speed, 1.0f));
    }

    public void die()
    {

        died = true;
        rend.material.SetFloat("_deactivated", 1);
        light_red.SetActive(false);
        light_green.SetActive(false);
        camera.GetComponent<Animator>().enabled = false;
        CancelInvoke();
        
        if (alarm.clip != null)
        {
            alarm.Stop();
        }
    }

    public void spawnEnemy()
    {

        GameObject newEnemy = Instantiate(enemyToSpawn, spawnSpot.transform.position, Quaternion.identity) as GameObject;
        newEnemy.GetComponent<enemy_ai>().Unlishthemonster();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !fired && !died)
        {
            cameraFieldOfView.enabled = false;
            TriggedAlarmEV.Invoke(1, false);
            rend.material.SetFloat("_state", 1);
            light_red.SetActive(true);
            light_green.SetActive(false);
            fired = true;
            InvokeRepeating("spawnEnemy", 1f, spawnTime);
            player.GetComponent<hero_controller>().heroExpressions(3);

            if (alarm.clip != null)
            {
                alarm.Play(0);
            }
        }
    }
}
