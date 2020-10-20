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
    public int damage;

    private bool reloading;
    public bool autoShooting;


    private float nextTimeToFire;

    private bool lookingRight;

    private Vector2 dir;


    private float angle;

    public Transform firepoint;
    public Transform RfirePoint;
    public Transform Lfirepoint;
    public Transform mouse;
    public GameObject bulletPrefab;
    public Rigidbody2D rb;

    public Sprite weaponsprite;
    public SpriteRenderer sprite;
    public SpriteRenderer FirePointSprite;

    public RangedWeaponStats weapon;

    public int whatWeapon;

    public PlayerHealth health;
    public SniperSounds sniper;
    public ShotgunSounds shotgun;
    public AutoSounds auto;
    public SemiSounds semi;
    public PistolSounds pistol;

    public AudioClip[] Shooting;
    public AudioClip[] Empty;
    public AudioClip[] Reloading;
    public AudioClip[] DoneReload;


    private AudioSource audioCenter;


    private void Start()
    {
        
       
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        damage = weapon.dmg;
        firepoint = RfirePoint;
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

        audioCenter = GameObject.Find("AudioCenter").GetComponent<AudioSource>();
        Debug.Log(audioCenter);

        switch (whatWeapon)
        {
            case 0:
                Shooting = AudioManager.pShot;
                Empty = AudioManager.pEmpty;
                Reloading = AudioManager.pReload;
                DoneReload = AudioManager.pDoneReload;
                return;
            case 1:
                Shooting = AudioManager.sShot;
                Empty = AudioManager.sEmpty;
                Reloading = AudioManager.sReload;
                DoneReload = AudioManager.sDoneReload;
                return;
            case 2:
                Shooting = AudioManager.aShot;
                Empty = AudioManager.aEmpty;
                Reloading = AudioManager.aReload;
                DoneReload = AudioManager.aDoneReload;
                return;
            case 3:
                Shooting = AudioManager.ssShot;
                Empty = AudioManager.ssEmpty;
                Reloading = AudioManager.ssReload;
                return;       
            case 4:
                Shooting = AudioManager.sssShot;
                Empty = AudioManager.sssEmpty;
                Reloading = AudioManager.sssReload;
                return;
        }





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
            firepoint = RfirePoint;

        }
        else if (mouse.position.x < transform.position.x && !lookingRight)
        {
            lookingRight = !lookingRight;
            sprite.flipY = true;
            FirePointSprite.flipY = true;
            firepoint = Lfirepoint;
        }

        dir = (mouse.position - transform.position);
        dir.Normalize();


        angle = Mathf.Atan2(dir.y, dir.x);
        transform.rotation = quaternion.Euler(0, 0, angle);

        if(reloading && Input.GetMouseButtonDown(0))
        {
            audioCenter.PlayOneShot(Empty[UnityEngine.Random.Range(0, Empty.Length)]);
            audioCenter.volume = 0.1f;
        }

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
        audioCenter.PlayOneShot(Shooting[UnityEngine.Random.Range(0, Shooting.Length)]);
        audioCenter.volume = 0.1f;
        for (int i = 0; i < numberOfShots; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firepoint.position, transform.rotation);
            bullet.transform.Rotate(0, 0, UnityEngine.Random.Range(minSpread, maxSpread));
            Bullet bulletstats = bullet.GetComponent<Bullet>();

            bulletstats.headShotDamage = weapon.headShotDamage;
            bulletstats.dmg = weapon.dmg;
            bulletstats.BulletSpeed = weapon.BulletSpeed;
            bulletstats.range = weapon.range;
            bulletstats.startLocation = firepoint.position;
            bulletstats.piercingReduction = weapon.piercingReduction;
            bulletstats.force = force;
            bulletstats.dir = dir;
        }
        ammo--;
        rb.velocity += -dir * force;
    }



    IEnumerator Reload()
    {
        ammo = 0;
        audioCenter.PlayOneShot(Reloading[UnityEngine.Random.Range(0, Reloading.Length)]);
        audioCenter.volume = 0.1f;
        
        health.TakeDamage(weapon.lifeSteal);
        reloading = true;
        yield return new WaitForSeconds(reloadSpeed);
        audioCenter.PlayOneShot(DoneReload[UnityEngine.Random.Range(0, DoneReload.Length)]);
        ammo = maxAmmo;
        reloading = false;
    }
}

public class PistolSound
{
}