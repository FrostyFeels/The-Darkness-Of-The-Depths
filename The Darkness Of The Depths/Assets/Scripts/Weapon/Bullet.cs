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
    public int shieldDmg;
    public float range;
    public float force;
    public Vector2 dir;
    public Vector3 startLocation;

    private AudioSource audioCenter;

    public AudioClip wallHit;
    public AudioClip enemyHit;

    public void Start()
    {
        audioCenter = GameObject.Find("AudioCenter").GetComponent<AudioSource>();
        wallHit = AudioManager.wallHit;
        enemyHit = AudioManager.enemyHit;
    }


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
                audioCenter.PlayOneShot(enemyHit);
                Destroy(gameObject);
                
            }

            if (hit.collider.CompareTag("Boss"))
            {
                BossHealth health = hit.collider.gameObject.GetComponentInParent<BossHealth>();
                health.TakeDamage(dmg);
                audioCenter.PlayOneShot(enemyHit);
                Destroy(gameObject);

            }
            if (hit.collider.CompareTag("Shield"))
            {
                MeleeWeapon shield = hit.collider.gameObject.GetComponent<MeleeWeapon>();
                shield.takeDamage(shieldDmg);
                Destroy(gameObject);
                audioCenter.PlayOneShot(wallHit);
                
                
            }

            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Floor"))
            {
                audioCenter.PlayOneShot(wallHit);
                Destroy(gameObject);
                
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            audioCenter.PlayOneShot(enemyHit);
            EnemyHealth health = collision.gameObject.GetComponentInParent<EnemyHealth>();
            health.TakeDamage(dmg, dir, force);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Boss"))
        {
            BossHealth health = collision.gameObject.GetComponentInParent<BossHealth>();
            health.TakeDamage(dmg);
            audioCenter.PlayOneShot(enemyHit);
            Destroy(gameObject);

        }
        if (collision.CompareTag("Shield"))
        {
            audioCenter.PlayOneShot(wallHit);
            MeleeWeapon shield = collision.gameObject.GetComponent<MeleeWeapon>();
            shield.takeDamage(shieldDmg);
            Destroy(gameObject);

        }
        if (collision.CompareTag("Wall") || collision.CompareTag("Floor"))
        {
            audioCenter.PlayOneShot(wallHit);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {

        transform.Translate(Vector3.right * BulletSpeed * Time.fixedDeltaTime);
    }
}
