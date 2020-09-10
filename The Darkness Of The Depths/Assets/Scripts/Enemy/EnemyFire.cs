using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public Vector2 dir;
    public Transform player;
    public Transform firepoint;
    public Transform gun;
    public GameObject bulletPrefab;
    public float angle;
    public float nextTimeToFire;
    public float minSpread;
    public float maxSpread;
    public float BulletSpeed;
    public float reloadingTIme;
    public int damage;
    public float AttackSpeed;
    public int NumberOfShots;
    public int ammo;
    public int range;
    public float waitingToFireTime;



    public EnemyRangedStatsManager weapon;
    

    public LineRenderer line;
    public EnemyAI ai;
    [SerializeField] private LayerMask platfromLayerMask;


    public void Start()
    {
        reloadingTIme = weapon.stats.reloadSpeed;
        AttackSpeed = weapon.stats.AttackSpeed;
        minSpread = weapon.stats.minSpread;
        maxSpread = weapon.stats.maxSpread;
        BulletSpeed = weapon.stats.BulletSpeed;
        damage = weapon.stats.damage;
        NumberOfShots = weapon.stats.NumberOfShots;
        ammo = weapon.stats.ammo;
        range = weapon.stats.range;
        
    }

    void Update()
    {
        dir = (player.position - firepoint.position).normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (ai.ai == EnemyAI.Ai.Reloading)
            return;

        if(Time.time >= nextTimeToFire && ai.ai != EnemyAI.Ai.Shooting)
        {
            nextTimeToFire = Time.time + (1 / AttackSpeed) + waitingToFireTime;
            StartCoroutine(Aiming());
        }
    }

    IEnumerator Aiming()
    {

        yield return new WaitForSeconds(waitingToFireTime);       
        ai.ai = EnemyAI.Ai.Shooting;
        Shoot();       
    }


    void Shoot()
    {
        GameObject enemyBullet = Instantiate(bulletPrefab, firepoint.position, gun.transform.rotation);
        EnemyBullet stats = enemyBullet.GetComponent<EnemyBullet>();
        stats.BulletSpeed = BulletSpeed;
        stats.dmg = damage;
        stats.startLocation = firepoint.position;
        stats.range = range;


        ai.ai = EnemyAI.Ai.Running;
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(reloadingTIme);
        
    }




    





}
