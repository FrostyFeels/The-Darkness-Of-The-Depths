using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    EnemyAI ai;
    public EnemyRangedStatsManager stats;
    private EnemyMovement enemyMove;

    public float health;
    public float maxHealth;
    public float regen;
    public int armor;

    private void Start()
    {
       
        enemyMove = gameObject.GetComponent<EnemyMovement>();
        ai = gameObject.GetComponent<EnemyAI>();

        health = stats.stats.HP;
        armor = stats.stats.armor;
        maxHealth = health;

    }
    public void TakeDamage(int damage)
    {
        health -= (damage - armor);
        Debug.Log("yes");
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
            Destroy(gameObject);
        }
    }
}
