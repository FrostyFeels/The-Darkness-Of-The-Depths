using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed;
    public float dmg;



    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, .5f);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.CompareTag("Enemy"))
            {
                //EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
                //enemy.TakeDamage(dmg);
            }


            if (hit.collider.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }

        }
    }
}
