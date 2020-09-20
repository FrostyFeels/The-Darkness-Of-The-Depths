using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
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
    public string enemyName;

    private EnemyRangedStatsManager weapon;
    public EnemyAI ai;
    public SpriteRenderer sprite;


    public void Start()
    {
        weapon = GameObject.FindGameObjectWithTag(enemyName).GetComponent<EnemyRangedStatsManager>();
        player = weapon.player.transform;
        sprite.sprite = weapon.stats.weaponSprite;
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

        if(ammo <= 0)
        {
            StartCoroutine(Reloading());
            ai.ai = EnemyAI.Ai.Reloading;
        }

        if(Time.time >= nextTimeToFire && ai.ai != EnemyAI.Ai.Attacking)
        {
            nextTimeToFire = Time.time + (1 / AttackSpeed) + waitingToFireTime;
            StartCoroutine(Aiming());
        }


    }

    IEnumerator Aiming()
    {

        yield return new WaitForSeconds(waitingToFireTime);       
        ai.ai = EnemyAI.Ai.Attacking;
        Shoot();       
    }


    void Shoot()
    {
        for (int i = 0; i < NumberOfShots; i++)
        {
            GameObject enemyBullet = Instantiate(bulletPrefab, firepoint.position, gun.transform.rotation);
            enemyBullet.transform.Rotate(0, 0, UnityEngine.Random.Range(minSpread, maxSpread));
            EnemyBullet stats = enemyBullet.GetComponent<EnemyBullet>();
            stats.BulletSpeed = BulletSpeed;
            stats.dmg = damage;
            stats.startLocation = firepoint.position;
            stats.range = range;
        }
        ammo--; 

        ai.ai = EnemyAI.Ai.Running;
    }

    IEnumerator Reloading()
    {
        
        yield return new WaitForSeconds(reloadingTIme);
        ammo = weapon.stats.ammo;
        ai.ai = EnemyAI.Ai.Running;
    }




    





}
