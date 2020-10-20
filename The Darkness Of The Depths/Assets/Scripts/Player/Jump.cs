using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private float fJumpPressedRemember;
    public float fJumpPressedRememberTime = 0.2f;

    public int jumps = 1;
    public int jumpsLeft;
    public float jumpforce = 5f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    bool isJumping, isChargeJumping = false;

    public float timeToCharge;
    public float chargeTime;

    Slide slide;
    Grappeling grapple;

    [SerializeField] private LayerMask platfromLayerMask;

    void Start()
    {
        
        slide = GetComponent<Slide>();
        grapple = GetComponent<Grappeling>();
        jumpsLeft = jumps;
        bc = GetComponentInChildren<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    } 
    void Update()
    {

        if (slide.isSliding || grapple.grappling)
            return;
        fJumpPressedRemember -= Time.deltaTime;

        if (Input.GetButtonDown("Jump")) //renews timer when pressed jump
        {
            fJumpPressedRemember = fJumpPressedRememberTime;
            
        }

        if (IsGrounded() && (fJumpPressedRemember > 0 && Input.GetButtonUp("Jump"))) //Checks if player is connected to the ground then changes the velocity to a value
        {
            isJumping = true;
            fJumpPressedRemember = 0f;
            jumpsLeft = jumps;           
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpsLeft = jumps;
            isJumping = true;
        }

        if (!IsGrounded() && jumpsLeft > 0 && Input.GetButtonDown("Jump") && StaticManager.doubleJump)
        {
           
            isJumping = true;
            jumpsLeft--;
        }

        if (Input.GetKey(KeyCode.X) && IsGrounded())
        {
            chargeTime += Time.deltaTime;
        }                   
        else if (chargeTime >= timeToCharge)
        {
            isChargeJumping = true;
            jumpsLeft--;
        }
        else
        {
            chargeTime = 0f;
        }
     
     
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 2f;
        }
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            isJumping = false;
        }
        if(isChargeJumping)
        {
            chargeTime = 0;
            isChargeJumping = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * 3);
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, platfromLayerMask);
        return raycasthit2d.collider != null;
    }
}
