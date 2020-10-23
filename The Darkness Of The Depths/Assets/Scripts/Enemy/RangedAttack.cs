using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public Vector2 dir, setDirection;
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
    public float shieldDamage;
    public float waitingToFireTime;
    public string enemyName;

    private EnemyRangedStatsManager weapon;
    public EnemyAI ai;
    public SpriteRenderer sprite;

    public LineRenderer aimLine;
    public LineRenderer shootLine;

    public bool aiming, shooting, realshooting;
    [SerializeField] private LayerMask enemymask;

    public float colorAddition;

    public Color color;
    public Color spawncolor;
   


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
        shieldDamage = weapon.stats.shieldDamage;
        spawncolor = color;


        colorAddition = 255 / waitingToFireTime * Time.deltaTime;
    }

    void Update()
    {
        Debug.DrawRay(firepoint.position, dir, Color.red, 2f);
        dir = (player.position - firepoint.position).normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;



        if (ai.sniper && !realshooting)
        {
            gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else if (!ai.sniper)
        {
            gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }





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

        if(aiming)
        {
            aimLine.enabled = true;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, enemymask);
            if(ray.collider != null)
            {
                aimLine.SetPosition(0, firepoint.position);
                aimLine.SetPosition(1, ray.point);              
            }
        }          
        if(shooting)
        {          
            shootLine.enabled = true;
            realshooting = true;
            Debug.DrawRay(transform.position, setDirection, Color.black, 10f);

            RaycastHit2D ray = Physics2D.Raycast(transform.position, setDirection, Mathf.Infinity, enemymask);
            if (ray.collider != null)
            {
                shootLine.SetPosition(0, firepoint.position);
                shootLine.SetPosition(1, ray.point);                                       
            }

            color = shootLine.endColor;
            color.r += colorAddition *  Time.deltaTime;
            color.b -= colorAddition * Time.deltaTime;
            color.g -= colorAddition * Time.deltaTime;
            shootLine.endColor = color;
            shootLine.startColor = color;
        }
        else
        {
            realshooting = false;
        }


    }

    public void resetWeapon()
    {
        dir = Vector2.zero;
        angle = 0;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    IEnumerator Aiming()
    {
        if(ai.sniper)
        {
            aiming = true;
        }
        yield return new WaitForSeconds(waitingToFireTime);    
       
        if(ai.sniper)
        {
            ai.ai = EnemyAI.Ai.Attacking;
            setDirection = dir;
            Debug.Log("Courotine direction: " + setDirection);
            shooting = true;
            aiming = false;
            aimLine.enabled = false;
            
            Debug.Log(shooting);
            
            Debug.DrawRay(firepoint.position, setDirection * 20, Color.black, 10f);
            yield return new WaitForSeconds(waitingToFireTime);
            shooting = false;
            shootLine.enabled = false;
            shootLine.endColor = spawncolor;
            shootLine.startColor = spawncolor;
            Shoot();
        }
        else
        {
            ai.ai = EnemyAI.Ai.Attacking;
            Shoot();
        }
             
    }


    public void Shoot()
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
            stats.shieldDamage = shieldDamage;
        }
        ammo--;
        realshooting = false;
        ai.ai = EnemyAI.Ai.searching;
    }

    IEnumerator Reloading()
    {
        
        yield return new WaitForSeconds(reloadingTIme);
        ammo = weapon.stats.ammo;
        ai.ai = EnemyAI.Ai.searching;
    }




    





}
