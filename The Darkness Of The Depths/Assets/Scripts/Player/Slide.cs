using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Slide : MonoBehaviour
{
    private Rigidbody2D rb;

    public BoxCollider2D normalCollider;

    public float slideSpeed;
    public bool isSliding = false;
    private bool givenDirection = false;
    public float timer;
    public float wait;
    public float rotation;

    Jump jump;
    Movement mv;

    private Movement player;
    [SerializeField] private LayerMask platfromLayerMask;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = gameObject.GetComponent<Movement>();
        jump = GetComponent<Jump>();
        mv = GetComponent<Movement>();
    }
    void Update()
    {
        if (player.IsGrounded() && Input.GetKeyDown(KeyCode.S) && !isSliding)
        {
            isSliding = true;
            transform.Rotate(0f, 0f, rotation * mv.movement.x);
            jump.enabled = false;
            mv.enabled = false;

        }

        if (timer > wait && !HeadFree())
        {
            transform.Rotate(0f, 0f, 0f);
            isSliding = false;
            jump.enabled = true;
            mv.enabled = true;

            timer = 0f;
        }
    }
    void FixedUpdate()
    {
        if (isSliding)
        {
            timer += Time.fixedDeltaTime;
            if (!givenDirection)
            {
                if (player.movement.x > 0)
                {
                    rb.velocity = Vector2.right * slideSpeed;
                }
                else if (player.movement.x < 0)
                {
                    rb.velocity = Vector2.left * slideSpeed;
                }
            }
        }
    }
    public bool HeadFree()
    {
        RaycastHit2D raycasthit2d = Physics2D.BoxCast(normalCollider.bounds.center, normalCollider.bounds.size, 0f, Vector2.up, .1f, platfromLayerMask);
        return raycasthit2d.collider != null;
    }

}

