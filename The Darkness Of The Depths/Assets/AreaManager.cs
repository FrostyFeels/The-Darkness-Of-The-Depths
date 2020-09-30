using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public Movement player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Movement>();
    }

    
    void Update()
    {
        
    }
}
