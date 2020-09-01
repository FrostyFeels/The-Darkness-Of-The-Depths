using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Weapon : MonoBehaviour
{
    public float AS;
    public float reloadSpeed;
    public float ammo;
    public float maxAmmo;
    public float minSpread;
    public float maxSpread;
    public int numberOfShots;
    public float force;


    private bool reloading;
    public bool autoShooting;


    private float nextTimeToFire;

    private bool lookingRight;

    private Vector3 dir;
    private Vector2 weaponKnockbackDir;

    private float angle;

    public Transform firePoint;
    public Transform mouse;
    public Transform player;
    public GameObject bulletPrefab;
    public Rigidbody2D rb;

    public Sprite weaponsprite;
    public SpriteRenderer sprite;
    public SpriteRenderer FirePointSprite;

    public RangedWeaponStats weapon;





    private void Start()
    {
        ammo = weapon.ammo;
        AS = weapon.fireRate;
        reloadSpeed = weapon.reloadSpeed;
        maxAmmo = weapon.ammo;
        minSpread = weapon.minSpread;
        maxSpread = weapon.maxSpread;
        numberOfShots = weapon.NumberOfShots;
        force = weapon.knockBackForce;
        autoShooting = weapon.autoShooting;
        weaponsprite = weapon.weaponSprite;




    }

    private void OnEnable()
    {
        reloading = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (mouse.position.x > transform.position.x && lookingRight)
        {
            lookingRight = !lookingRight;
            sprite.flipY = false;
            FirePointSprite.flipY = false;

        }
        else if (mouse.position.x < transform.position.x && !lookingRight)
        {
            lookingRight = !lookingRight;
            sprite.flipY = true;
            FirePointSprite.flipY = true;
        }

        dir = (mouse.position - transform.position);
        dir.Normalize();
        weaponKnockbackDir = (mouse.position - transform.position);
        weaponKnockbackDir.Normalize();


        angle = Mathf.Atan2(dir.y, dir.x);
        transform.rotation = quaternion.Euler(0, 0, angle);

        if (reloading)
            return;



        if (Input.GetKeyDown(KeyCode.R) && ammo != maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }
        if (ammo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }


        if (!autoShooting)
        {
            if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / AS;
                Shoot();


            }
        }
        else
        {
            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / AS;
                Shoot();

            }
        }
    }


    void Shoot()
    {
        for (int i = 0; i < numberOfShots; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            bullet.transform.Rotate(0, 0, UnityEngine.Random.Range(minSpread, maxSpread));
            Bullet stats = bullet.GetComponent<Bullet>();
            stats.dmg = weapon.dmg;
            stats.speed = weapon.BulletSpeed;

        }
        ammo--;
        rb.velocity += -weaponKnockbackDir * force;
    }



    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadSpeed);

        ammo = maxAmmo;
        reloading = false;


    }
}
