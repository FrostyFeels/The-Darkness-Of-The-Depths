using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class WallSliding : MonoBehaviour
{

    Rigidbody2D rb;
    BoxCollider2D bc;
    public PhysicsMaterial2D slippery;
    public Movement movement;
    public float  jumpTimer;
    public float  jumpWait;
    public bool jumped = false;
    bool walljump = false;
    [SerializeField] private LayerMask platfromLayerMask;
    [SerializeField] private LayerMask grounded;
    private void Start()
    {
        bc = GetComponentInChildren<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {

        if(againstWall() || IsGrounded() && walljump)
        {     
                rb.sharedMaterial = slippery;
                rb.velocity = new Vector2(rb.velocity.x, -5f);
        } else
        {
            rb.sharedMaterial = new PhysicsMaterial2D("Normal");
        }

        if(againstWall() && Input.GetButtonDown("Jump") && StaticManager.wallJump)
        {
            movement.enabled = false;
            walljump = true;         
            jumped = true;
        }

        if(jumped)
        {
            jumpTimer += Time.deltaTime;
            if(jumpTimer > jumpWait)
            {
                movement.enabled = true;
                jumped = false;
                jumpTimer = 0;
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (walljump)
        {
            movement.moveSpeed = 0;
            rb.velocity = new Vector2((-Input.GetAxisRaw("Horizontal") * 15 ), 25);
            walljump = false;
        }
        
    }


    bool againstWall()
    {
        if (movement.movement.x > 0)
        {
            RaycastHit2D raycasthit2d = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.right, .05f, platfromLayerMask);
            return raycasthit2d.collider != null;
        }
        else
        {
            RaycastHit2D raycasthit2d = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.left, .05f, platfromLayerMask);
            return raycasthit2d.collider != null;
        }


    }

    public bool IsGrounded()
    {
        if (movement.movement.x > 0)
        {
            RaycastHit2D raycasthit2d = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.right, .05f, grounded);
            return raycasthit2d.collider != null;
        }
        else
        {
            RaycastHit2D raycasthit2d = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.left, .05f, grounded);
            return raycasthit2d.collider != null;
        }
    }
}
