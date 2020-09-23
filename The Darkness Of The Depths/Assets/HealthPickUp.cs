using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    private Transform player;
    public int range;
    public float moveSpeed;
    public int healAmount;
    public bool isQuitting = false;


    private void Start()
    {
        player = GameObject.Find("SpiderPlayer").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = player.position;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, moveSpeed * Time.fixedDeltaTime);
        if(player.transform.position.x - transform.position.x < range && player.transform.position.x - transform.position.x > -range)
        {

            transform.position = smoothPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponentInParent<PlayerHealth>();
            health.GetHP(healAmount);
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if(isQuitting)
        {
            Destroy(gameObject);
        }
    }


}
