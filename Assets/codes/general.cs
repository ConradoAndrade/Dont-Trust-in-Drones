using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class general : Singleton<general>
{

    protected general() { } // Protect the constructor!

    public string globalVar;
    public int lifeHero = 100;
    public int staminaHero = 100;
    public int globalHeroFace = 0;
    public int globalHeroStars = 10;

    public int globalHeroDeats = 0;

    public bool obj1;
    public bool obj2;
    public bool obj3;


    public bool obj21;
    public bool obj22;
    public bool obj23;


    void Awake()
    {
        Debug.Log("Awoke Singleton Instance: " + gameObject.GetInstanceID());
    }

    public void DamageHero(int val)
    {
        if (lifeHero >=1)
        {
            lifeHero -= val;
        }else if(lifeHero <= 0)
        {
            lifeHero = 0;
        }

    }

    public void ResetHero()
    {
        lifeHero = 100;
        staminaHero = 100;
    }




}