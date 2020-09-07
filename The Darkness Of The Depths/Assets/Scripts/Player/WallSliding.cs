using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class WallSliding : MonoBehaviour
{

    Rigidbody2D rb;
    BoxCollider2D bc;
    public PhysicsMaterial2D slippery;
    public Movement movement;
    [SerializeField] private LayerMask platfromLayerMask;
    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(againstWall())
        {
            rb.sharedMaterial = slippery;
        } else
        {
            rb.sharedMaterial = new PhysicsMaterial2D("Normal");
        }
        
    }


    bool againstWall()
    {
        if (movement.movement.x > 0)
        {
            RaycastHit2D raycasthit2d = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.right, .01f, platfromLayerMask);
            return raycasthit2d.collider != null;
        }
        else
        {
            RaycastHit2D raycasthit2d = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.left, .01f, platfromLayerMask);
            return raycasthit2d.collider != null;
        }


    }
}
