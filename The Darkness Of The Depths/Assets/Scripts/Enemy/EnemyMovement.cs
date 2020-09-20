using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    public Transform player;
    public Rigidbody2D rb;
    public EnemyAI AI;
    public Transform meleeWeapon;
    public Transform spawnPoint;

    public float averagespeed;
    public float speed;
    public float direction;
    public float distance;
    public float range;
    public SpriteRenderer sprite;
    private bool lookingRight;

    private EnemyRangedStatsManager rangedEnemy;
    private EnemyMeleeStatsManager meleeEnemy;
    public RangedEnemyStats rangedStats;
    public MeleeEnemyStats meleeStats;


    void Start()
    {
        AI = gameObject.GetComponent<EnemyAI>();
        
        if (AI.ranged)
        {
            rangedEnemy = GameObject.FindGameObjectWithTag(AI.rangedAttack.enemyName).GetComponent<EnemyRangedStatsManager>();
            rangedStats = rangedEnemy.stats;
            player = rangedEnemy.player.transform;
            range = rangedStats.attackRange;
            speed = rangedStats.movementSpeed;
            averagespeed = rangedStats.movementSpeed;
        }
        else
        {
            meleeEnemy = GameObject.FindGameObjectWithTag(AI.meleeAttack.enemyName).GetComponent<EnemyMeleeStatsManager>();
            meleeStats = meleeEnemy.stats;
            player = meleeEnemy.player.transform;
            range = meleeStats.attackRange;
            speed = meleeStats.movementSpeed;
            averagespeed = meleeStats.movementSpeed;
        }
        
        
        
   
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AI.ranged)
        {


            if (player.position.x > transform.position.x && lookingRight)
            {
                lookingRight = !lookingRight;
                sprite.flipY = false;
                

            }
            else if (player.position.x < transform.position.x && !lookingRight)
            {
                lookingRight = !lookingRight;
                sprite.flipY = true;
                
            }
        }

        if (AI.melee)
        {


            if (player.position.x > transform.position.x && lookingRight)
            {
                lookingRight = !lookingRight;
                meleeWeapon.Rotate(0f, 0f, -180f);
                

            }
            else if (player.position.x < transform.position.x && !lookingRight)
            {
                lookingRight = !lookingRight;
                meleeWeapon.Rotate(0f, 0f, 180f);

            }
        }

        distance = (transform.position.x - player.position.x);
        if(distance > 0) { direction = 1; }
        if(distance < 0) { direction = -1; }

  

        if (AI.ai == EnemyAI.Ai.aiming || AI.ai == EnemyAI.Ai.Attacking || AI.ai == EnemyAI.Ai.Reloading)
            return;

        if ((transform.position.x - player.position.x > range || transform.position.x - player.position.x < -range))
        {
            speed = averagespeed * 2;
        }

        if (transform.position.x - player.position.x < range && transform.position.x - player.position.x > -range)
        {             
            if(AI.ranged)
            {
                AI.ai = EnemyAI.Ai.aiming;
            }

            if(AI.melee)
            {
                AI.ai = EnemyAI.Ai.Attacking;
            }
            
  
        }
    }


    public void FixedUpdate()
    {
        rb.velocity = new Vector2(-direction * speed, rb.velocity.y);
    }
}
