using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldcollision : MonoBehaviour
{

    public BossAi ai;
    public Rigidbody2D rigidbody;
    public int dmg;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Debug.Log("OwO");
            Rigidbody2D rb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
            PlayerHealth health = collision.gameObject.GetComponentInParent<PlayerHealth>();
            health.TakeDamage(dmg);
            rb.AddForce(new Vector2(-100f, 100f), ForceMode2D.Impulse);
            rigidbody.gravityScale = 100;
            ai.states = BossAi.States.afk;
            ai.shield.enabled = false;
            ai.shield.charging = false;
            gameObject.SetActive(false);
            
        }


    }

}
