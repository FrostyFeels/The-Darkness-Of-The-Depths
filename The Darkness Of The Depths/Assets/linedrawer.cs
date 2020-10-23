using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linedrawer : MonoBehaviour
{
    public BossAi boss;
    public BossWeapon weapon;
    public Vector2 dir;
    public Vector2 speed;
    public LineRenderer lr;
    public Transform endpoint;

    public bool giveValue = false;
 
    void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<BossAi>();
        weapon = GameObject.Find("BossBody").GetComponent<BossWeapon>();

        //lr.SetPosition(0, Vector2.zero);
        //lr.SetPosition(1, Vector2.zero);

    }


    void Update()
    {
        //transform.Translate(Vector3.right * 50f * Time.deltaTime);
        lr.SetPosition(1, transform.position);
 
            if (Vector3.Distance(transform.position, endpoint.position) > -1 && Vector3.Distance(transform.position, endpoint.position) < 1)
            {
                speed = Vector2.zero;
                if(!giveValue)
                {
                    weapon.colorLines = true;
                    giveValue = true;
                }
                
            }

        if(weapon.despawnCubes)
        {   
            Destroy(gameObject);
        }
        

    }

    private void FixedUpdate()
    {
        transform.Translate(speed);
    }
}
