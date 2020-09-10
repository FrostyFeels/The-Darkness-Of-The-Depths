using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappeling : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 dir;

    private float angle;
    private Vector3 playerDir;

    public GameObject grapplePrefab;
    public Transform Grapplestart;
    public Transform Mouse;
    public Transform Player;
    public Rigidbody2D playerRB;


    private Vector3 grapplehitpos;

    GameObject target;

    public bool grappling = false;


    public float grappleForce = 10f;
    public float jumpforce = 5f;


    private LineRenderer lr;

    private void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        lr.enabled = false;

    }


    // Update is called once per frame
    void Update()
    {
        dir = Mouse.position - Grapplestart.position;
        dir.Normalize();
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Grapplestart.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Shot grappeling hook");
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Space) && grappling)
        {
            //playerRB.velocity = new Vector2(playerRB.velocity.x, jumpforce);
            grappling = false;
        }

        if (target != null)
        {
            lr.SetPosition(0, Player.position);
            lr.SetPosition(1, grapplehitpos);
            float dist = Vector3.Distance(grapplehitpos, Player.transform.position);

            if (dist <= 1f && dist >= -1f)
            {
                grappling = false;
                playerRB.gravityScale = 1;
            }
        }
        else
        {

        }

        if (!grappling)
        {
            target = null;
            lr.enabled = false;
        }
    }


    private void Shoot()
    {
        GameObject grapple = Instantiate(grapplePrefab, Grapplestart.position, Grapplestart.rotation);
    }

    public void TargetHit(GameObject hit, Vector3 hitposition)
    {
        playerRB.gravityScale = 0;
        target = hit;
        grapplehitpos = hitposition;
        lr.enabled = true;
        grappling = true;

        playerDir = grapplehitpos - Grapplestart.position;
        playerDir.Normalize();
        playerRB.velocity = Vector2.zero;
        playerRB.velocity = playerDir * grappleForce;
    }
}
