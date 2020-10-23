using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    public float speed;
    public int direction;
    public Rigidbody2D rb;
    public Transform player;
    public SpriteRenderer spriterender;

    public BossAi boss;

    public bool charging = false;
    
    void Start()
    {
       
    }

    private void OnEnable()
    {
        spriterender.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!charging)
        {
            if (transform.position.x - player.position.x > 0)
            {
                direction = -1;
            }
            else if (transform.position.x - player.position.x < 0)
            {
                direction = 1;
            }
            charging = true;
        }
    }

    public void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * direction, 0);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("WallFloor"))
        {
            rb.gravityScale = 100;
            boss.states = BossAi.States.afk;
            boss.shield.enabled = false;
            boss.shieldobject.SetActive(false);
            charging = false;
        }
    }



}
