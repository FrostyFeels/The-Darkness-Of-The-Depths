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
    
    void Start()
    {
        
        AI = gameObject.GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
       
        direction = (transform.position.x - player.position.x);
        if(direction > 0) { direction = 1; }
        if(direction < 0) { direction = -1; }

        if(transform.position.x - player.position.x > range || transform.position.x - player.position.x < -range)
        {
            Debug.Log("running");
            AI.ai = EnemyAI.Ai.Running;
        } else
        {
            AI.ai = EnemyAI.Ai.Walking;
        }
        
    }


    public void FixedUpdate()
    {
        rb.velocity = new Vector2(-direction * speed, rb.velocity.y);
    }
}
