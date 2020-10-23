using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BossWeapon : MonoBehaviour
{

    public Vector2 dir, shotgunDir;
    public Vector2[] setDirections;
    public int dmg;
    public float angle;

    public Transform player;
    public Transform firepoint;
    public Transform RfirePoint, Lfirepoint;
    public Transform gun;
    public GameObject bulletPrefab;

    public float minSpread;
    public float maxSpread;
    public float BulletSpeed;
    public float reloadingTIme;
    public int damage;
    public int NumberOfShots;
    public float shieldDamage;
    public Sprite[] weaponSprites;
    public SpriteRenderer sprite;

    public BossAi ai;

    [SerializeField] private LayerMask enemymask;
    [SerializeField] private LayerMask shootmask;
    [SerializeField] private LayerMask playermask;

    public float colorAddition;
    public Color color, shotColor;
    public Color startColor ,startShotColor;
    public bool lookingRight;
    public bool lockDir;

    public bool colorLines, colorShotLines;

    public bool despawnCubes = false;

    public List<GameObject> topShots = new List<GameObject>();
    public List<GameObject> botShots = new List<GameObject>();
    public List<LineRenderer> lrSniperAuto = new List<LineRenderer>();
    public GameObject linePrefab;

    public List<Transform> stillSniperSpawns = new List<Transform>();
    public List<LineRenderer> lrSniperStill = new List<LineRenderer>();

    public float timer;

    public int loops = 0;
    public bool autoSniper = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < topShots.Count; i++)
        {
            lrSniperAuto[i] = topShots[i].GetComponent<LineRenderer>();
        }
        for (int i = 0; i < stillSniperSpawns.Count; i++)
        {
            lrSniperStill[i] = stillSniperSpawns[i].GetComponent<LineRenderer>();
        }
        colorAddition = 255 / 5 * Time.deltaTime;


    }
    void Update()
    {

        if (player.position.x > transform.position.x && lookingRight)
        {
            lookingRight = !lookingRight;
            sprite.flipY = false;
            firepoint = RfirePoint;

        }
        else if (player.position.x < transform.position.x && !lookingRight)
        {
            lookingRight = !lookingRight;
            sprite.flipY = true;
            firepoint = Lfirepoint;
        }

        if (ai.states == BossAi.States.Shotgun)
        {
            sprite.sprite = weaponSprites[0];
            sprite.enabled = true;
        }
        else if (ai.states == BossAi.States.sniperStill || ai.states == BossAi.States.SniperAuto)
        {
            sprite.sprite = weaponSprites[1];
            sprite.enabled = true;
        }
        else
        {
            sprite.enabled = false;
        }

       
        dir = (player.position - firepoint.position).normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (!lockDir)
        {
            gun.transform.rotation = Quaternion.Euler(0, 0, angle);
        }


        if (colorLines)
        {
            color = lrSniperAuto[0].endColor;    
                color.r += colorAddition * Time.deltaTime;     
                color.b -= colorAddition * Time.deltaTime;                           
                color.g -= colorAddition * Time.deltaTime;
           
         

            for (int i = 0; i < lrSniperAuto.Count; i++)
            {
                lrSniperAuto[i].endColor = color;
                lrSniperAuto[i].startColor = color;             
            }

            if(lrSniperAuto[lrSniperAuto.Count - 1].endColor.r >= 1)
            {

                colorLines = false;
                for (int i = 0; i < lrSniperAuto.Count; i++)
                {               
                    Vector2 newDir = (lrSniperAuto[i].GetPosition(0) - lrSniperAuto[i].GetPosition(1)).normalized;
                    
                    RaycastHit2D hitinfo = Physics2D.Raycast(lrSniperAuto[i].transform.position, -newDir, Mathf.Infinity, playermask);
                    Debug.Log(hitinfo.collider);
                    if (hitinfo.collider != null)
                    {                       
                        if (hitinfo.collider.CompareTag("Player"))
                        {
                            PlayerHealth health = hitinfo.collider.gameObject.GetComponentInParent<PlayerHealth>();
                            health.TakeDamage(dmg * 6);
                        }                    
                    }

                    shotColor.r = 255;
                    shotColor.g = 255;
                    shotColor.b = 0;

                    lrSniperAuto[i].endColor = shotColor;
                    lrSniperAuto[i].startColor = shotColor;

                    
                             
                }
                StartCoroutine(removeLines());
  


            }
           


        }     
    }

    public IEnumerator removeLines()
    {
        yield return new WaitForSeconds(.25f);
        for (int i = 0; i < lrSniperAuto.Count; i++)
        {
            lrSniperAuto[i].endColor = startColor;
            lrSniperAuto[i].startColor = startColor;
            color = startColor;
            lrSniperAuto[i].enabled = false;
        }
        despawnCubes = true;

        if (loops <= 6 && autoSniper)
        {
            shuffle();
            AutoSniper();
            autoSniper = false;
        } else
        {
            ai.sniperfloor.SetActive(true);
            loops = 0;
            ai.globalLight.intensity = 1f;
            ai.transform.position = new Vector2(0f, -13f);
            ai.states = BossAi.States.afk;
        }

    }
    public IEnumerator FireShotgun()
    {

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < NumberOfShots; i++)
        {
            GameObject enemyBullet = Instantiate(bulletPrefab, firepoint.position, gun.transform.rotation);
            enemyBullet.transform.Rotate(0, 0, Random.Range(minSpread, maxSpread));
            EnemyBullet stats = enemyBullet.GetComponent<EnemyBullet>();



            stats.BulletSpeed = BulletSpeed;
            stats.dmg = damage;
            stats.startLocation = firepoint.position;
            stats.shieldDamage = shieldDamage;
        }
        lockDir = false;
        ai.rb.gravityScale = 100f;

        yield return new WaitForSeconds(.5f);
        ai.states = BossAi.States.afk;
    }
    public void shuffle()
    {
        for (int i = 0; i < botShots.Count; i++)
        {
            GameObject temp = botShots[i];
            int randomIndex = Random.Range(i, botShots.Count);
            botShots[i] = botShots[randomIndex];
            botShots[randomIndex] = temp;
        }
        
    }
    public IEnumerator SniperShot(Transform sniperspawns, Vector2 newDir, LineRenderer lr, float timer)
    {
        lr.enabled = true;
        yield return new WaitForSeconds(timer);
        RaycastHit2D hitinfo = Physics2D.Raycast(sniperspawns.position, newDir, Mathf.Infinity, shootmask);
        if(hitinfo)
        {
            if (hitinfo.collider.CompareTag("Player"))
            {
                PlayerHealth health = hitinfo.collider.gameObject.GetComponentInParent<PlayerHealth>();
                health.TakeDamage(dmg);
            }

            lr.SetPosition(0, sniperspawns.position);
            lr.SetPosition(1, hitinfo.point);
            shotColor.r = 255;
            shotColor.g = 255;
            shotColor.b = 0;

            lr.endColor = shotColor;  
            lr.startColor = shotColor;
            

        }

        yield return new WaitForSeconds(.25f);
        lr.enabled = false;
        lr.startColor = startColor;
        lr.endColor = startColor;
        ai.states = BossAi.States.afk;
        


    }
    public void AutoSniper()
    {
        ai.transform.position = new Vector3(1000f, 1000f, 0);
        loops++;
        autoSniper = true;
        despawnCubes = false;
        for (int i = 0; i < topShots.Count; i++)
        {
            lrSniperAuto[i].enabled = true;

            GameObject hiddenCube = Instantiate(linePrefab, topShots[i].transform.position, topShots[i].transform.rotation);
            Rigidbody2D rb = hiddenCube.gameObject.GetComponent<Rigidbody2D>();        

            Vector2 speed = new Vector2(botShots[i].transform.position.x - topShots[i].transform.position.x, botShots[i].transform.position.y - topShots[i].transform.position.y);
            speed = speed / 45;
            
            linedrawer linedrawer = hiddenCube.gameObject.GetComponent<linedrawer>();
            linedrawer.speed = speed;    
            linedrawer.giveValue = false;         
            linedrawer.lr = lrSniperAuto[i];
            linedrawer.endpoint = botShots[i].transform;
            
            lrSniperAuto[i].SetPosition(0, topShots[i].transform.position);          
        }    
    }
    public IEnumerator StillSniper(Transform sniperspawns, int i)
    {
     
        ai.transform.position = sniperspawns.position;
        Vector2 newDir = (player.position - transform.position).normalized;
        RaycastHit2D ray = Physics2D.Raycast(sniperspawns.position, newDir, 1000, enemymask);

        lrSniperStill[i].enabled = true;
        lrSniperStill[i].SetPosition(0, transform.position);
        lrSniperStill[i].SetPosition(1, ray.point);
        yield return new WaitForSeconds(.25f);

        if(i < stillSniperSpawns.Count -1)
        {
            StartCoroutine(StillSniper(stillSniperSpawns[i + 1].transform, i + 1));
        }
            
            StartCoroutine(SniperShot(sniperspawns, newDir, lrSniperStill[i], 1.75f - (i * .25f)));
        
    }
}
