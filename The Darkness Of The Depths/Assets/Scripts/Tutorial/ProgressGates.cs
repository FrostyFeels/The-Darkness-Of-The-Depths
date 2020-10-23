using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressGates : MonoBehaviour
{
    public static int unlockNumber = 0;
    public GameObject switcher, UI;
    public bool FinalCheckPoint;
    public TextTutorial text;

    public Tutorial tutorial;


    private void OnTriggerEnter2D(Collider2D collision)
    {  
        if (collision.CompareTag("Player"))
        {
            
            unlockNumber++;
            ChangeAbility();
        }
    }

    public void ChangeAbility()
    {
        text.textnumber++;
        text.spawnText();
        Debug.Log(unlockNumber);
        switch (unlockNumber)
        {
            case 1:
                StaticManager.doubleJump = false;
                StaticManager.slide = true;
                break;
            case 2:
                StaticManager.slide = false;
                break;
            case 3:
                StaticManager.wallJump = true;
                break;
            case 4:
                StaticManager.wallJump = false;
                StaticManager.powerJump = true;
                break;
            case 5:
                StaticManager.powerJump = false;
                StaticManager.grappleHook = true;
                break;
            case 6:
                StaticManager.grappleHook = false;
                switcher.SetActive(true);
                break;

        }

        if(FinalCheckPoint)
        {
            switcher.SetActive(true);
            UI.SetActive(true);
            

            StaticManager.UnlockWeapon("Sniper");
            StaticManager.UnlockWeapon("Shotgun");
            StaticManager.UnlockWeapon("AutoRifle");
            StaticManager.UnlockWeapon("SemiAutoRifle");
            Debug.Log(StaticManager.shotgun);
            Debug.Log(StaticManager.sniper);
            Debug.Log(StaticManager.autoRifle);
            Debug.Log(StaticManager.semiAutoRifle);
            tutorial.SpawnEnemy();
        }
        Destroy(gameObject);
    }
}
