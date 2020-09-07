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
    public float angle;
    public bool aiming;
    public bool laser;
    public bool shooting;
    
    public float timeToFire;

    public LineRenderer line;
    public EnemyAI ai;


    // Start is called before the first frame update
    void Start()
    {
       
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(laser)
        {
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, player.position);
        }
  
        

       
        dir = (player.position - transform.position);
        dir.Normalize();
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        gun.transform.rotation = Quaternion.Euler(0f, 0f, angle); 


        if(!aiming && !shooting)
        {
            StartCoroutine(Aiming());
            Debug.Log("I should not run rnl");
        }


    }

    IEnumerator Aiming()
    {
        Debug.Log("Aiming");
        aiming = true;
        yield return new WaitForSeconds(1f);
        shooting = true;
        ai.ai = EnemyAI.Ai.Shooting;
        ai.aiming = false;
        aiming = false;
        StartCoroutine(shoot());
        
    }


    IEnumerator shoot()
    {
        Debug.Log("Shooting");
        shooting = true;
     
        yield return new WaitForSeconds(1f);
        ai.shooting = false;
        shooting = false;
        laser = false;
        ai.ai = EnemyAI.Ai.Running;
       

    }




}
