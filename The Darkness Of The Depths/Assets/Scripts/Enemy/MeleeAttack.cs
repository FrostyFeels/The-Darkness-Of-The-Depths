using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public EnemyAI ai;

    private EnemyMeleeStatsManager weapon;
    private MeleeEnemyStats stats;
    public string enemyName;
    public float swingspeed;
    public int dmg;
    public Transform Sword;
    public float Timer;
    public float waitTimer;
    public bool attacking = false;
 

   
    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag(enemyName).GetComponent<EnemyMeleeStatsManager>();
        stats = weapon.stats;
        swingspeed = stats.swingSpeed;
        dmg = stats.damage;  
    }

    public IEnumerator Reloading()
    {
        Timer = 0;
        Sword.transform.position = ai.movement.spawnPoint.position;
        yield return new WaitForSeconds(1f);
        ai.ai = EnemyAI.Ai.Running;
    }

    private void FixedUpdate()
    {
        if (attacking)
        {
            if (Timer < waitTimer)
            {
                Timer += Time.fixedDeltaTime;
                if (ai.movement.direction < 0)
                {
                    Sword.transform.position += new Vector3(0.5f, 0f, 0f);           
                }
                if (ai.movement.direction > 0)
                {
                    Sword.transform.position += new Vector3(-0.5f, 0f, 0f);
                }
            }
            else
            {
                attacking = false;
                StartCoroutine(Reloading());
                ai.ai = EnemyAI.Ai.Reloading;               
            }
        }
    }
}
