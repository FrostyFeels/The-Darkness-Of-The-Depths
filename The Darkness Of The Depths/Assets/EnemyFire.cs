using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public Vector2 dir;
    public Vector2 aimdir;
    public Transform player;
    public Vector3 playerposition;
    public Transform firepoint;
    public Transform gun;
    public GameObject bulletPrefab;
    public float angle;
    public float timeToFire;

    public LineRenderer line;
    public EnemyAI ai;
    [SerializeField] private LayerMask platfromLayerMask;
    [SerializeField] private LayerMask PlayerLayerMask;


    // Start is called before the first frame update
    void Start()
    {
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        dir = (player.position - firepoint.position).normalized;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    
           
        RaycastHit2D rayhit = Physics2D.Raycast(firepoint.position, dir, Mathf.Infinity, platfromLayerMask);
        drawLine(rayhit.collider.transform.position);
 
        gun.transform.rotation = Quaternion.Euler(0f, 0f, angle); 
        if(!ai.aiming && !ai.shooting)
        {
            StartCoroutine(Aiming());
        }
    }

    IEnumerator Aiming()
    {
        ai.aiming = true;
        yield return new WaitForSeconds(timeToFire);       
        ai.ai = EnemyAI.Ai.Shooting;
        ai.shooting = true;
        ai.aiming = false;
        StartCoroutine(shoot());       
    }


    IEnumerator shoot()
    {
        yield return new WaitForSeconds(3f);
        ai.shooting = false;
        ai.ai = EnemyAI.Ai.Running;    
    }



    public void drawLine(Vector3 position)
    { 
        line.SetPosition(0, firepoint.position);
        line.SetPosition(1, position);
    }

    





}
