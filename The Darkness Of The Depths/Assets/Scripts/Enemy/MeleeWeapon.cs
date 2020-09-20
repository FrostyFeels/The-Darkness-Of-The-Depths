using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    EnemyAI ai;
    public MeleeAttack attack;
    public MeleeEnemyStats meleeStats;
    private EnemyMeleeStatsManager meleeEnemy;

    private int dmg;

    // Start is called before the first frame update
    void Start()
    {        
        meleeEnemy = GameObject.FindGameObjectWithTag(attack.enemyName).GetComponent<EnemyMeleeStatsManager>();
        meleeStats = meleeEnemy.stats;
        ai = gameObject.GetComponentInParent<EnemyAI>();
        dmg = meleeStats.damage;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ai.ai == EnemyAI.Ai.Attacking)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
               
                PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();                
                ai.ai = EnemyAI.Ai.Reloading;
                health.TakeDamage(dmg);
            }
        }
    }


}
