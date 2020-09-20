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
    public Vector3 startLocation;



    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, .5f);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth health = hit.collider.gameObject.GetComponentInParent<EnemyHealth>();
                Debug.Log(hit.collider.name);
                health.TakeDamage(dmg);
                Destroy(gameObject);
            }


            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Floor"))
            {
                Destroy(gameObject);
            }

        }

    }

    void FixedUpdate()
    {

        transform.Translate(Vector3.right * BulletSpeed * Time.fixedDeltaTime);
    }
}
