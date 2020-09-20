using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockAmount : MonoBehaviour
{
    private Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "UnlocksLeft: " + StaticManager.unlocksLeft.ToString();
    }
}
