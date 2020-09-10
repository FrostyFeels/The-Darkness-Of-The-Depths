using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;
    EnemyAI AI;
    public float averagespeed;
    public float speed;
    public float direction;
    public float range;
    public SpriteRenderer sprite;
    private bool lookingRight;


    void Start()
    {
        
        AI = gameObject.GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
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

        direction = (transform.position.x - player.position.x);
        if(direction > 0) { direction = 1; }
        if(direction < 0) { direction = -1; }

  

        if (AI.ai == EnemyAI.Ai.aiming || AI.ai == EnemyAI.Ai.Shooting || AI.ai == EnemyAI.Ai.Reloading)
            return;

        if ((transform.position.x - player.position.x > range || transform.position.x - player.position.x < -range))
        {
    

            AI.ai = EnemyAI.Ai.Running;

        }

        if (transform.position.x - player.position.x < range && transform.position.x - player.position.x > -range)
        {
           
    
            AI.ai = EnemyAI.Ai.aiming;
  
        }
    }


    public void FixedUpdate()
    {
        rb.velocity = new Vector2(-direction * speed, rb.velocity.y);
    }
}
