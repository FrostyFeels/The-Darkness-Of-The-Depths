using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    EnemyAI ai;
    public MeleeAttack attack;
    public MeleeEnemyStats meleeStats;
    private EnemyMeleeStatsManager meleeEnemy;
    public int shieldHp;

    private int dmg;

    // Start is called before the first frame update
    void Start()
    {        
        meleeEnemy = GameObject.FindGameObjectWithTag(attack.enemyName).GetComponent<EnemyMeleeStatsManager>();
        meleeStats = meleeEnemy.stats;
        ai = gameObject.GetComponentInParent<EnemyAI>();
        dmg = meleeStats.damage;
    }

    public void Update()
    {
        if(ai.tank)
        {
            if(shieldHp <= 0)
            {
                attack.enabled = false;
                Destroy(gameObject);
            }
        }
    }

    public void takeDamage(int dmg)
    {
        shieldHp -= dmg;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ai.ai == EnemyAI.Ai.Attacking)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
               if(ai.tank)
               {
                    Rigidbody2D rb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
                    if(ai.movement.direction == 1)
                    {
                        rb.AddForce(new Vector2(100, 100), ForceMode2D.Impulse);
                    }
                    else if(ai.movement.direction == -1)
                    {
                        rb.AddForce(new Vector2(-100, 100), ForceMode2D.Impulse);
                    }
               }
               PlayerHealth health = collision.gameObject.GetComponentInParent<PlayerHealth>();                
               ai.ai = EnemyAI.Ai.Reloading;
               health.TakeDamage(dmg);
            }
        }
    }


}
