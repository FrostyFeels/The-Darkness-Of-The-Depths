using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    EnemyAI ai;
    WaveManager manager;
    public EnemyRangedStatsManager rangedStats;
    public EnemyMeleeStatsManager meleeStats;
    private EnemyMovement enemyMove;

    public float health;
    public float maxHealth;
    public float regen;
    public int armor;

    private void Start()
    {

        ai = gameObject.GetComponent<EnemyAI>();
        manager = GameObject.Find("SpawnManager").GetComponent<WaveManager>();
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
    public void TakeDamage(int damage)
    {
        health -= (damage - armor);
      
    }

    public void GetHP(int healthAmount)
    {
        health += healthAmount;
    }

    public void Injured()
    {
        enemyMove.averagespeed = 3;
    }

    public void NotInjured()
    {
        enemyMove.averagespeed = 6;
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
            manager.kill++; 
            Destroy(gameObject);
        }
    }
}
