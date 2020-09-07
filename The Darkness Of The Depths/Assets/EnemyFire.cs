using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    private Vector3 dir;
    public Transform player;
    public Transform firepoint;
    public Transform gun;
    public GameObject bulletPrefab;
    public float range;
    public float angle;
    private bool lookingRight;

    public LineRenderer line;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (player.position.x > transform.position.x && lookingRight)
        {
            lookingRight = !lookingRight;
            sprite.flipY = false;


        }
        else if (player.position.x < transform.position.x && !lookingRight)
        {
            lookingRight = !lookingRight;
            sprite.flipY = true;

        }



        dir = (player.position - transform.position);
        dir.Normalize();
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        gun.transform.rotation = Quaternion.Euler(0f, 0f, angle); 
    }
}
