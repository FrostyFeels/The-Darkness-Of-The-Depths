using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    int currentWeapon = 0;

    public GameObject player;
    private Rigidbody2D rb;
    void Start()
    {
        SelectWeapon();
        rb = player.GetComponent<Rigidbody2D>();
    }



    // Update is called once per frame
    void Update()
    {
        transform.position = rb.position;
        int prevWeapon = currentWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }

        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }
        if (prevWeapon != currentWeapon)
        {
            SelectWeapon();
        }

    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform aweapon in transform)
        {
            if (i == currentWeapon)
            {
                aweapon.gameObject.SetActive(true);
            }
            else
            {
                aweapon.gameObject.SetActive(false);
            }
            i++;
        }
        {

        }
    }
}
