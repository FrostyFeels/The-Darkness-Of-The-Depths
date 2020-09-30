using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : MonoBehaviour
{
    public Transform rightBlock, leftBlock;
    public Transform blockLocation;
    public SpriteRenderer shieldSprite;
    public GameObject shield;
    private Movement playerMove;
    void Start()
    {
       
        playerMove = GetComponent<Movement>();
        blockLocation = rightBlock;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            shield.transform.position = blockLocation.position;
            shieldSprite.enabled = true;
        } else
        {
            shieldSprite.enabled = false;  
        }
    }
}
