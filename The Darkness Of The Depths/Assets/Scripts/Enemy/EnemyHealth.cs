﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    EnemyAI ai;
    WaveManager manager;
    public EnemyRangedStatsManager rangedStats;
    public EnemyMeleeStatsManager meleeStats;
    private EnemyMovement enemyMove;
    Rigidbody2D rb;
    public GameObject healthPrefab;
    public float health;
    public float maxHealth;
    public float regen;
    public int armor;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ai = gameObject.GetComponent<EnemyAI>();
       
        if (ai.ranged)
        {
            rangedStats = GameObject.FindGameObjectWithTag(ai.rangedAttack.enemyName).GetComponent<EnemyRangedStatsManager>();
            health = rangedStats.stats.HP;
            armor = rangedStats.stats.armor;
            maxHealth = health;
        }
        else
        {
            meleeStats = GameObject.FindGameObjectWithTag(ai.meleeAttack.enemyName).GetComponent<EnemyMeleeStatsManager>();
            health = meleeStats.stats.HP;
            armor = meleeStats.stats.armor;
            maxHealth = health;
        }
        
        enemyMove = gameObject.GetComponent<EnemyMovement>();
  

    }
    public void TakeDamage(int damage, Vector2 dir, float force)
    {
        health -= (damage - armor);
        enemyMove.enabled = false;
        rb.velocity = Vector2.zero;
        rb.velocity += dir * force;
        StartCoroutine(NotGettingKnockbacked());
    }

    public void GetHP(int healthAmount)
    {
        health += healthAmount;
        
    }

    public void Injured()
    {
        enemyMove.averagespeed = 3;
        StartCoroutine(NotInjured());
    }

    IEnumerator NotInjured()
    {
        yield return new WaitForSeconds(3f);
        enemyMove.averagespeed = 6;
    }

    IEnumerator NotGettingKnockbacked()
    {
        yield return new WaitForSeconds(.5f);
        Injured();
        enemyMove.enabled = true;
    }


    public void FixedUpdate()
    {
        if (health <= 150)
        {
            health += regen;
        }

    }
    public void Update()
    {
        if (health <= 0)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject health = Instantiate(healthPrefab, transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0f), Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }



}
