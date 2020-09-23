using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    public Weapon weapon;
    public Text Ammo;
    public Text Health;
    


    // Update is called once per frame
    void Update()
    {
        Ammo.text = weapon.ammo.ToString() + " / " + weapon.maxAmmo.ToString();
    }
}
