using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurret : MonoBehaviour
{
    public GameObject prefab;
    public Vector2 dir;
    public float angle = -180f;

    public Transform player;
    public Transform spawnPoint;

    public int minSpread;
    public int maxSpread;
    public float bulletSpeed;
    public int dmg;

    public float AS;
    public float timeToFire;

    public int bulletAmount;

    public BossAi ai;

    private void OnEnable()
    {
        transform.position = spawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {

        
        transform.rotation = Quaternion.Euler(0f, 0f, angle);


        if(Time.time >= timeToFire)
        {
            timeToFire = Time.time + AS;
            Fire();
        }

        angle = angle + 45 * Time.deltaTime;

        if(angle >= 0)
        {
            ai.states = BossAi.States.afk;
            gameObject.SetActive(false);
        }
    }


    public void Fire()
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject enemyBullet = Instantiate(prefab, transform.position, transform.rotation);
            enemyBullet.transform.Rotate(0, 0, Random.Range(minSpread, maxSpread));
            EnemyBullet stats = enemyBullet.GetComponent<EnemyBullet>();
            stats.BulletSpeed = bulletSpeed;
            stats.dmg = dmg;
            stats.shieldDamage = 2;
         }

    }

    
}
