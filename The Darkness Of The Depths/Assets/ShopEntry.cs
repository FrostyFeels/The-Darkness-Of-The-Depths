using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopEntry : MonoBehaviour
{
    public WaveManager wave;
    public GameObject shopText;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            shopText.SetActive(true);
            Debug.Log("CanLeave");
            if(Input.GetKey(KeyCode.Q))
            {

                if (wave.waveNumber >= wave.maxWave)
                {
                    Debug.Log("LET ME LEAVE");
                    StaticManager.unlocksLeft = 2;
                    SceneManager.LoadScene("Shop");
                }

                
            }

            if (Input.GetKey(KeyCode.C))
            {
                    SceneManager.LoadScene(1);
                    StaticManager.unlocksLeft = 2;
            }
        }
    }
}
