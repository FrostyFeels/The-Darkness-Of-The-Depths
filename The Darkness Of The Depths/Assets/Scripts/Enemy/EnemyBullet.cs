using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float BulletSpeed;
    public int dmg;
    public float range;
    public Vector3 startLocation;
    PlayerHealth player;


    // Update is called once per frame

    private void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(Vector3.right * BulletSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, .5f);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.CompareTag("Player"))
            {
                PlayerHealth health = hit.collider.gameObject.GetComponent<PlayerHealth>();
                health.TakeDamage(dmg);
                Destroy(gameObject);
            }


            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Floor"))
            {
                Destroy(gameObject);
            }

        }
    }
}
