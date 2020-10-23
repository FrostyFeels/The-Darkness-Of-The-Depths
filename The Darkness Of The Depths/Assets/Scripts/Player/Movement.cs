using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{


    private Rigidbody2D rb;
    private BoxCollider2D bc;
    public Transform body;
    [SerializeField] private LayerMask platfromLayerMask;
    [SerializeField] private LayerMask EnemyLayerMask;
    public bool facingRight = true;
    public Vector3 movement;

    public float sprintMultiplayer = 1.5f;

    public float moveSpeed = 0f;
    public float jumpforce = 5f;
    public float maxSpeed = 15f;
    public float timeToReachMaxSpeed = 1f;
    float accelPerSec;

    Slide slide;
    Grappeling grapple;
    Blocking block;
    public float rotation;
    public List<Transform> startLocation = new List<Transform>();
    public int startNumber;

    public AudioClip[] moving;
    private AudioSource audioCenter;


    public bool firstspawn;

    bool isMoving = false;
    float walktimer;
    private float walkreset = .35f;

    public float yeetTimer;
    public float maxEnemyStandTime;

    public bool flying;
    public float flyTimer;
    public float maxFlyTime;

    private PlayerHealth health;




    void Start()
    {
        audioCenter = GameObject.Find("AudioCenter").GetComponent<AudioSource>();
        health = GameObject.Find("SpiderPlayer").GetComponent<PlayerHealth>();
        block = GetComponent<Blocking>();
        accelPerSec = maxSpeed / timeToReachMaxSpeed;
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponentInChildren<BoxCollider2D>();
        slide = GetComponentInChildren<Slide>();
        grapple = GetComponentInChildren<Grappeling>();

        startNumber = StaticManager.lastLevel;
        if(StaticManager.firstspawn)
        {
           transform.position = startLocation[startNumber].transform.position;         
        }

        StaticManager.firstspawn = true;
       
    }

    void Update()
    {
        if (OnEnemy())
        {
            yeetTimer += Time.deltaTime;
            if (yeetTimer >= maxEnemyStandTime)
            {
                flying = true;
                rb.AddForce(new Vector2(200f, 75f), ForceMode2D.Impulse);
                health.TakeDamage(30);
            }
        }
        else
        {
            yeetTimer = 0;
        }
        if (flying)
        {
            flyTimer += Time.deltaTime;
            if (flyTimer >= maxFlyTime)
            {
                flying = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Boss"); 
        }

        if (slide.isSliding || grapple.grappling || flying)
            return;

        

        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f); // send direction of player


 
      
            if (movement.x > 0)
            {
                isMoving = true;
                moveSpeed += accelPerSec * Time.deltaTime;
                moveSpeed = math.min(moveSpeed, maxSpeed);
                if(IsGrounded())
                {
                    if (Time.time >= walktimer)
                    {
                        moving = AudioManager.movement;
                        walktimer = Time.time + walkreset;
                        audioCenter.PlayOneShot(moving[UnityEngine.Random.Range(0, moving.Length)]);
                    }
                }                      
            }
            else if (movement.x < 0)
            {
                isMoving = true;
                moveSpeed -= accelPerSec * Time.deltaTime;
                moveSpeed = math.max(moveSpeed, -maxSpeed);
                if (IsGrounded())
                {
                    if (Time.time >= walktimer)
                    {
                        moving = AudioManager.movement;
                        walktimer = Time.time + walkreset;
                        audioCenter.PlayOneShot(moving[UnityEngine.Random.Range(0, moving.Length)]);
                    }
                }
            }
            else
            {
                isMoving = false;

            }
        


        if (Input.GetAxis("Horizontal") > 0 && facingRight)
        {
            Flip();           
            block.blockLocation = block.rightBlock.transform;
       
        }
        if (Input.GetAxis("Horizontal") < 0 && !facingRight)
        {          
            Flip();
            block.blockLocation = block.leftBlock;
           
        }

    }
    private void FixedUpdate()
    {
        if (slide.isSliding || grapple.grappling || flying)
            return;

        if (isMoving)
            {
                body.transform.rotation = quaternion.Euler(0f, 0f , rotation * -movement.x);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = new Vector2(moveSpeed * sprintMultiplayer, rb.velocity.y);
                
            }
            else { rb.velocity = new Vector2(moveSpeed, rb.velocity.y); }
        }

        if (!isMoving)
        {
            body.transform.rotation = quaternion.Euler(0f, 0f, 0f);
            if (moveSpeed > 1)
            {
                moveSpeed -= 0.5f;
            }
            else if (moveSpeed < -1)
            {
                moveSpeed += 0.5f;
            }
            else
            {
                moveSpeed = 0;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 360f, 0f);
    

    }
    public bool IsGrounded()
    {
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, platfromLayerMask);
        return raycasthit2d.collider != null;
    }
    public bool OnEnemy()
    {
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, EnemyLayerMask);;
        return raycasthit2d.collider != null;
    }

}
