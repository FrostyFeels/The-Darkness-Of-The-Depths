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
    public Vector3 vectorDistance;
    public float attackRange;
    public float range;
    public SpriteRenderer sprite;
    private bool lookingRight;

    private EnemyRangedStatsManager rangedEnemy;
    private EnemyMeleeStatsManager meleeEnemy;
    public RangedEnemyStats rangedStats;
    public MeleeEnemyStats meleeStats;

    public Transform groundCheck;
    public bool movingRight = true;

    [SerializeField] private LayerMask enemyLayermask;
    [SerializeField] private LayerMask playerMask;

    private Vector2 dir;


    void Start()
    {
        direction = 1;

        AI = gameObject.GetComponent<EnemyAI>();      
        if (AI.ranged)
        {
            rangedEnemy = GameObject.FindGameObjectWithTag(AI.rangedAttack.enemyName).GetComponent<EnemyRangedStatsManager>();
            rangedStats = rangedEnemy.stats;
            player = GameObject.Find("SpiderPlayer").transform;
            attackRange = rangedStats.attackRange;
            range = rangedStats.range;
            speed = rangedStats.movementSpeed;
            averagespeed = rangedStats.movementSpeed;
        }
        else
        {
            meleeEnemy = GameObject.FindGameObjectWithTag(AI.meleeAttack.enemyName).GetComponent<EnemyMeleeStatsManager>();
            meleeStats = meleeEnemy.stats;
            player = GameObject.Find("SpiderPlayer").transform;
            attackRange = meleeStats.attackRange;
            range = meleeStats.range;
            speed = meleeStats.movementSpeed;
            averagespeed = meleeStats.movementSpeed;
        }
        
    }

    void Update()
    {

        if (AI.ai == EnemyAI.Ai.Attacking || AI.ai == EnemyAI.Ai.aiming || AI.ai == EnemyAI.Ai.Reloading)
            return;
            dir = (player.position - transform.position).normalized;
        RaycastHit2D playerInfo = Physics2D.Raycast(transform.position, dir, range, playerMask);
        if (playerInfo.collider != null && playerInfo.collider.CompareTag("Player"))
        {
            AI.ai = EnemyAI.Ai.chasing;
        }
        else
        {
            AI.ai = EnemyAI.Ai.searching;
        }




        if (AI.ai == EnemyAI.Ai.chasing)
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 2.5f, enemyLayermask);
            if (groundInfo.collider)
            {
                if (transform.position.x - player.position.x > 0)
                {
                    direction = -1;
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }
                else
                {
                    direction = 1;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }




            vectorDistance = transform.position - player.position;
            distance = vectorDistance.x + vectorDistance.y + vectorDistance.z;

            if (distance < attackRange && distance > -attackRange)
            {
                if(AI.melee)
                {
                    AI.ai = EnemyAI.Ai.Attacking;
                }
                if(AI.ranged)
                {
                    AI.ai = EnemyAI.Ai.aiming;
                }
                
            }
        }   
    }

    


    public void FixedUpdate()
    {

        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 2.5f, enemyLayermask);
        RaycastHit2D wallInfo = Physics2D.Raycast(groundCheck.position, Vector2.zero, .1f, enemyLayermask);
        if (AI.ai == EnemyAI.Ai.searching)
        {
            if (groundInfo.collider == false || wallInfo.collider == true)
            {
                if (movingRight)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                    direction = -1;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                    direction = 1;
                }       
            }
        }

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
}
