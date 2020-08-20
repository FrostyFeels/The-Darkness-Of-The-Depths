using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int jumps = 1;
    public int jumpsLeft;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
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

    bool isMoving = false;


    void Start()
    {
        accelPerSec = maxSpeed / timeToReachMaxSpeed;
        jumpsLeft = jumps;
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        slide = GetComponent<Slide>();
    }

    void Update()
    {
        if (slide.isSliding)
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
        }
        if (Input.GetAxis("Horizontal") < 0 && facingRight)
        {
            Flip();
        }

    }
    private void FixedUpdate()
    {


        if (isMoving)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = new Vector2(moveSpeed * sprintMultiplayer, rb.velocity.y);
            }
            else { rb.velocity = new Vector2(moveSpeed, rb.velocity.y); }
        }

        if (!isMoving)
        {
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
