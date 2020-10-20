using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleShot : MonoBehaviour
{
 
    private Grappeling grapple;
    public float speed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        grapple = GameObject.Find("SpiderPlayer").GetComponent<Grappeling>();
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        Debug.Log(collision.gameObject);
        Debug.Log(grapple);


        if (collision.CompareTag("Floor"))
        {
            grapple.TargetHit(collision.gameObject, transform.position);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Wall"))
        {
            grapple.TargetHit(collision.gameObject, transform.position);
            Destroy(gameObject);
        }

    }
}
