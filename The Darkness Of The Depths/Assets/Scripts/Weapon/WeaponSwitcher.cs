using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    int currentWeapon = 0;
    string weaponName;

    public GameObject player;
    private Rigidbody2D rb;
    public PlayerStatsUI UI;
    private bool scrollUp;
    public int pistolWeapon;
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
        if(StaticManager.HasBoughAWeapon)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (currentWeapon >= transform.childCount - 1)
                {
                    currentWeapon = 0;
                    scrollUp = true;
                }
                else
                {
                    scrollUp = true;
                    currentWeapon++;
                }

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
                weaponName = aweapon.gameObject.name;
                if (StaticManager.CheckWeaponUnlock(weaponName))
                {
                    aweapon.gameObject.SetActive(true);

                    UI.weapon = aweapon.gameObject.GetComponent<Weapon>();
                   
                    
                } 
                else
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
            }
            else
            {
                aweapon.gameObject.SetActive(false);
            }
            i++;
        }

    }
}
