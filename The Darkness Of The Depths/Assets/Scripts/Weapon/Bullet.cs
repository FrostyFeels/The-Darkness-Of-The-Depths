using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int headShotDamage;
    public float piercingReduction;
    public float BulletSpeed;
    public bool hasPierced = false;
    public int dmg;
    public float range;
    public float force;
    public Vector2 dir;
    public Vector3 startLocation;



    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, .5f);

        if (hit.collider != null)
        {     
            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth health = hit.collider.gameObject.GetComponentInParent<EnemyHealth>();           
                health.TakeDamage(dmg, dir, force);
                Destroy(gameObject);
            }
            if(hit.collider.CompareTag("Shield"))
            {
                MeleeWeapon shield = hit.collider.gameObject.GetComponent<MeleeWeapon>();
                shield.takeDamage(dmg);
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
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth health = collision.gameObject.GetComponentInParent<EnemyHealth>();
            health.TakeDamage(dmg, dir, force);
            Destroy(gameObject);
        }
        if(collision.CompareTag("Shield"))
        {
            MeleeWeapon shield = collision.gameObject.GetComponent<MeleeWeapon>();
            shield.takeDamage(dmg);
            Destroy(gameObject);

        }
        if (collision.CompareTag("Wall") || collision.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {

        transform.Translate(Vector3.right * BulletSpeed * Time.fixedDeltaTime);
    }
}
