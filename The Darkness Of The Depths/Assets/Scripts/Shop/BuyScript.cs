using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyScript : MonoBehaviour
{
    public GameObject text;
    public Text prizeText;
    public GameObject prizeobject;
    public string weaponName;
    public string otherWeaponName;
    public TextLocation canvas;
    public int prize;

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
            prizeobject.SetActive(true);
            prizeText.text = "Prize: " + prize.ToString();
            if(Input.GetKey(KeyCode.X))
            {
                if(StaticManager.goldAmount > prize)
                {
                    Debug.Log(weaponName);
                    StaticManager.UnlockWeapon(weaponName);
                    StaticManager.goldAmount--;
                    canvas.SpawnText(weaponName);
                    text.SetActive(false);
                    prizeobject.SetActive(true);
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
            prizeobject.SetActive(false);
        }
    }
}
