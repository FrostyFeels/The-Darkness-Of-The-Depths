using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleShot : MonoBehaviour
{
 
    private Grappeling grapple;
    private readonly float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        grapple = GameObject.FindGameObjectWithTag("Player").GetComponent<Grappeling>();
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Floor"))
        {
            grapple.TargetHit(collision.gameObject, transform.position);
            Destroy(gameObject);
        }
    }
}
