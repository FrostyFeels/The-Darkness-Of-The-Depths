using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveShop : MonoBehaviour
{
    void Update()
    {
        if(StaticManager.unlocksLeft <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
