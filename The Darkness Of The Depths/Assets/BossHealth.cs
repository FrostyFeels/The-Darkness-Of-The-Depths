using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    Rigidbody2D rb;
    public float health;
    public float maxHealth;
    public BossAi ai;

    public bool phase1, phase2, phase3, phase4;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= (damage);
    }



    public void FixedUpdate()
    {
        if(health < (maxHealth / 4) * 3 && phase1 && ai.states == BossAi.States.afk)
        {
            phase2 = true;
            phase1 = false;
            ai.states = BossAi.States.SniperAuto;
        }

        if (health < (maxHealth / 2) && phase2 && phase2 && ai.states == BossAi.States.afk)
        {
            phase3 = true;
            phase2 = false;
            ai.states = BossAi.States.sniperStill;
        }
        if (health < (maxHealth / 4) && phase3 && phase3 && ai.states == BossAi.States.afk)
        {
            phase4 = true;
            phase3 = false;
            ai.states = BossAi.States.TurretSpawn;
        }
    }
    public void Update()
    {
         
    }
}
