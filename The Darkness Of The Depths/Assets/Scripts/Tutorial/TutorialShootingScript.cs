using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShootingScript : MonoBehaviour
{
    public float attackSpeed;
    public float Timer;
    public GameObject bulletPrefab;
    public float spped;

    public void Start()
    {
        
    }

    public void Update()
    {
        if (Time.time >= Timer)
        {
            Timer = Time.time + 1 / attackSpeed;
            Shoot();
        }
    }



    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        EnemyBullet enemynBullet = bullet.gameObject.GetComponent<EnemyBullet>();
        enemynBullet.BulletSpeed = 15f;
    }
}
