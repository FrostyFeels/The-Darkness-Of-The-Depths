using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float BulletSpeed = 15;
    public int dmg;
    public float range;
    public float shieldDamage;
    public Vector3 startLocation;
    PlayerHealth player;
    private Blocking playerBlock;
    [SerializeField] private LayerMask BulletLayerMask;


    // Update is called once per frame

    private void Start()
    {
        playerBlock = GameObject.FindGameObjectWithTag("Player").GetComponent<Blocking>();
    }
    void Update()
    {
        transform.Translate(Vector3.right * BulletSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, .5f, BulletLayerMask);
        if (hit.collider != null)
        {
            
            if (hit.collider.CompareTag("Shield"))
            {
                Debug.Log("Hit");
                playerBlock.TakeDamage(shieldDamage);
                Destroy(gameObject);

            }


            if (hit.collider.CompareTag("Player"))
            {

                PlayerHealth health = hit.collider.gameObject.GetComponentInParent<PlayerHealth>();
                health.TakeDamage(dmg);
                Destroy(gameObject);
            }




            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Floor"))
            {
                Destroy(gameObject);
            }

        }


        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shield"))
        {
            Debug.Log("Hit");
            playerBlock.TakeDamage(shieldDamage);
            Destroy(gameObject);
        }


        if (collision.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponentInParent<PlayerHealth>();
            health.TakeDamage(dmg);
            Destroy(gameObject);
        }




        if (collision.CompareTag("Wall") || collision.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
