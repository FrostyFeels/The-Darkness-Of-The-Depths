using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : MonoBehaviour
{
    public Transform rightBlock, leftBlock;
    public Transform blockLocation;
    public SpriteRenderer shieldSprite;
    public Transform shield;
    private Movement playerMove;
    public float hp;
    public bool dead = false;
    void Start()
    {
       
        playerMove = GetComponent<Movement>();
        blockLocation = rightBlock;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q) && !dead)
        {
            shield.localPosition = blockLocation.localPosition;                 
            shieldSprite.enabled = true;           
        } else
        {
            shieldSprite.enabled = false;
            shield.localPosition = new Vector3(1000, 1000, 1000f);
        }

        if(hp < 0)
        {
            Die();
        }
    }

    public void TakeDamage(float hit)
    {
        hp -= hit;
    }

    public void Die()
    {
        dead = true;
    }


    
}
