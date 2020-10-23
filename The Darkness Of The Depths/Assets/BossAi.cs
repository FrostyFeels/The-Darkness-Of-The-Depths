using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class BossAi : MonoBehaviour
{
    public Transform player;
    public BossWeapon shotgun;
    public BossShield shield;
    public BossTurret turret;
    public BossHealth health;

    public GameObject turretObject;
    public GameObject shieldobject;
    [HideInInspector] public Rigidbody2D rb;

    public Light2D globalLight;
    public GameObject sniperfloor;

    public SpriteRenderer eye1s, eye2s;

    public Color yellow, blue;

    public bool dimLevel, dimLevel2;

    public Movement move;

    public int turretSpeed;
    public bool spawnTurret;

    public float tpRange;
    public float chargeRange;

    public int whatAttack;

    public int platformHeight;

    public Transform[] spawnpoints;

    public int currentShotgun;
    public int maxShotgun;


    public enum States
    {
        Teleport,
        Shotgun,
        SniperAuto,
        sniperStill,
        ShieldBash,
        TurretSpawn,
        afk,
        readyToAttack
    }

    public States states;
    public States currentstate;

    public float pauzeTime;

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        currentstate = states;
        stateSwitch();
    }

    public void Update()
    {
        if (currentstate != states)
        {

            stateSwitch();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            states = States.Teleport;
        }
        if(states != States.ShieldBash)
        {
            if (transform.position.x - player.position.x > 0)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        if(dimLevel)
        {
            rb.gravityScale = 0;
            globalLight.intensity = globalLight.intensity - Time.deltaTime;
            if(globalLight.intensity <= 0.1f)
            {
                globalLight.intensity = 0.1f;
                dimLevel = false;
                SniperAuto();

            }
        }
        if (dimLevel2)
        {
            rb.gravityScale = 0;
            globalLight.intensity = globalLight.intensity - Time.deltaTime;
            if (globalLight.intensity <= 0.1f)
            {
                globalLight.intensity = 0.1f;
                dimLevel2 = false;
                StartCoroutine(shotgun.StillSniper(shotgun.stillSniperSpawns[0], 0));
            }
        }
        if (spawnTurret)
        {
            turretObject.transform.Translate(Vector2.up * turretSpeed * Time.deltaTime);
        }
    }


    public void stateSwitch()
    {
        switch (states)
        {
            case States.Teleport:
                currentstate = states;
                StartCoroutine(Teleport());               
                break;
            case States.Shotgun:
                currentstate = states;
                StartCoroutine(shotgun.FireShotgun());
                pauzeTime = 1f;
                break;
            case States.SniperAuto:
                currentstate = states;
                dimLevel = true;
                pauzeTime = 2f;
               
                break;
            case States.sniperStill:
                currentstate = states;
                dimLevel2 = true;
                sniperfloor.SetActive(false);
                pauzeTime = 2f;
                break;
            case States.ShieldBash:
                currentstate = states;
                StartCoroutine(Shield());
                shieldobject.SetActive(true);
                pauzeTime = 1f;
                break;
            case States.TurretSpawn:
                currentstate = states;
                StartCoroutine(MoveTurret());
                spawnTurret = true;
                turretObject.SetActive(true);
                pauzeTime = 1.5f;
                break;
            case States.afk:

                currentstate = states;
                StartCoroutine(StopAfk());
                rb.gravityScale = 100f;
                break;
            case States.readyToAttack:
                currentstate = states;
                
                WhatAttack();
                break;
        }
        
    }


    public void WhatAttack()
    {

        if (health.phase2 || health.phase3 || health.phase4)
        {
            whatAttack = Random.Range(0, 2);
        }
        else
        {
            if (player.position.y > platformHeight && transform.position.y < platformHeight - 10)
            {
                if (player.position.x > transform.position.x)
                {
                    transform.position = spawnpoints[0].position;
                }
                else if (player.position.x < transform.position.x - 10)
                {
                    transform.position = spawnpoints[1].position;
                }
            }

            if (player.position.y < platformHeight && transform.position.y > platformHeight)
            {
                if (player.position.x > transform.position.x)
                {
                    transform.position = spawnpoints[2].position;
                }
                else if (player.position.x < transform.position.x - 10)
                {
                    transform.position = spawnpoints[3].position;
                }
            }


            if (transform.position.x - player.position.x > tpRange || transform.position.x - player.position.x < -tpRange)
            {
                states = States.Teleport;
                currentShotgun = 0;
            }
            else if (transform.position.x - player.position.x < chargeRange && transform.position.x - player.position.x > -chargeRange)
            {
                states = States.ShieldBash;
                currentShotgun = 0;
            }
            else
            {
                if(currentShotgun < maxShotgun)
                {
                    states = States.Shotgun;
                    currentShotgun++;
                }
                else
                {
                    currentShotgun = 0;
                    states = States.Teleport;
                }
    
            }

        }
       


        if(whatAttack == 0)
        {
            if (transform.position.x - player.position.x > tpRange || transform.position.x - player.position.x < -tpRange)
            {
                states = States.Teleport;
                currentShotgun = 0;
            }
            else if (transform.position.x - player.position.x < chargeRange && transform.position.x - player.position.x > -chargeRange)
            {
                states = States.ShieldBash;
                currentShotgun = 0;
            }
            else
            {
                if (currentShotgun < maxShotgun)
                {
                    states = States.Shotgun;
                    currentShotgun++;
                }
                else
                {
                    currentShotgun = 0;
                    states = States.Teleport;
                }
            }
        }
        if(whatAttack == 1)
        {
            currentShotgun = 0;
            if (health.phase2)
            {
                int j = Random.Range(0, 1);
                switch (j)
                {
                    case 0:
                        states = States.SniperAuto;
                        break;
                }
            }
            if (health.phase3)
            {
                int j = Random.Range(0, 2);
                switch (j)
                {
                    case 0:
                        states = States.SniperAuto;
                        break;
                    case 1:
                        states = States.sniperStill;
                        break;

                }
            }
            if (health.phase4)
            {
                int j = Random.Range(0, 3);
                switch (j)
                {
                    case 0:
                        states = States.SniperAuto;
                        break;
                    case 1:
                        states = States.sniperStill;
                        break;
                    case 2:
                        states = States.TurretSpawn;
                        break;
                }
            }
        }    
    }
    IEnumerator StopAfk()
    {
        yield return new WaitForSeconds(pauzeTime);
        if(states == States.afk)
        {
            states = States.readyToAttack;
        }
        
    }
    IEnumerator Shield()
    {
        yield return new WaitForSeconds(.5f);
        shield.enabled = true;
        
        rb.gravityScale = 0;
    }
    IEnumerator Teleport()
    {
        states = States.Shotgun;
        eye1s.color = yellow;
        eye2s.color = yellow;

        yield return new WaitForSeconds(.5f);
        eye1s.color = blue;
        eye2s.color = blue;
        rb.gravityScale = 0;
        if(move.facingRight)
        {
            transform.position = player.position + new Vector3(10f, 5f, 0f);
        }
        else
        {
            transform.position = player.position + new Vector3(-10f, 5f, 0f);
        }
               
    }
    IEnumerator MoveTurret()
    {
        yield return new WaitForSeconds(1f);
        turret.enabled = true;
        spawnTurret = false;
    }
    
    void SniperAuto()
    {
        sniperfloor.SetActive(false);
        shotgun.shuffle();
        shotgun.AutoSniper();
    }
}
