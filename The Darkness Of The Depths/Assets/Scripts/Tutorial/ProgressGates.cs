using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressGates : MonoBehaviour
{
    public static int unlockNumber = 0;
    public GameObject switcher;
    public bool FinalCheckPoint;

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
        Debug.Log(unlockNumber);
        switch (unlockNumber)
        {
            case 1:
                StaticManager.doubleJump = false;
                StaticManager.slide = true;
                break;
            case 2:
                StaticManager.slide = false;
                //blocking
                break;
            case 3:
                StaticManager.wallJump = true;
                //blocking = false;
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
            SceneManager.LoadScene(1);
        }
        Debug.Log("Should destroy");
        Destroy(gameObject);
    }
}
