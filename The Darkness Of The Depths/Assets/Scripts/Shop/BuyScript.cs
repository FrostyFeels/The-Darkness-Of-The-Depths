using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyScript : MonoBehaviour
{
    public GameObject text;
    public string weaponName;
    public string otherWeaponName;
    public TextLocation canvas; 

    public void Start()
    {
        otherWeaponName = gameObject.name;
        if (StaticManager.CheckWeaponUnlock(otherWeaponName))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            text.SetActive(true);
            if(Input.GetKey(KeyCode.X))
            {
                if(StaticManager.unlocksLeft > 0)
                {
                    Debug.Log(weaponName);
                    StaticManager.UnlockWeapon(weaponName);
                    StaticManager.unlocksLeft--;
                    canvas.SpawnText(weaponName);
                    text.SetActive(false);
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            text.SetActive(false);
        }
    }
}
