using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class Movement : MonoBehaviour
{


    private Rigidbody2D rb;
    private BoxCollider2D bc;
    public Transform body;
    [SerializeField] private LayerMask platfromLayerMask;
    private bool facingRight = true;
    public Vector3 movement;

    public float sprintMultiplayer = 1.5f;

    public float moveSpeed = 0f;
    public float jumpforce = 5f;
    public float maxSpeed = 15f;
    public float timeToReachMaxSpeed = 1f;
    float accelPerSec;

    Slide slide;
    Grappeling grapple;
    //Blocking block;
    public float rotation;
   
    

    bool isMoving = false;

    


    void Start()
    {
        //block = GetComponent<Blocking>();
        accelPerSec = maxSpeed / timeToReachMaxSpeed;
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponentInChildren<BoxCollider2D>();
        slide = GetComponentInChildren<Slide>();
        grapple = GetComponentInChildren<Grappeling>();
    }

    void Update()
    {
        if (slide.isSliding || grapple.grappling)
            return;
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f); // send direction of player
      
            if (movement.x > 0)
            {
                isMoving = true;
                moveSpeed += accelPerSec * Time.deltaTime;
                moveSpeed = math.min(moveSpeed, maxSpeed);
            }
            else if (movement.x < 0)

            {
                isMoving = true;
                moveSpeed -= accelPerSec * Time.deltaTime;
                moveSpeed = math.max(moveSpeed, -maxSpeed);
            }
            else
            {
                isMoving = false;

            }
        


        if (Input.GetAxis("Horizontal") > 0 && !facingRight)
        {
            Flip();
            //block.blockLocation = block.leftBlock;
        }
        if (Input.GetAxis("Horizontal") < 0 && facingRight)
        {
            
            Flip();
            //block.blockLocation = block.rightBlock;
        }

    }
    private void FixedUpdate()
    {
        if (slide.isSliding || grapple.grappling)
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

}
