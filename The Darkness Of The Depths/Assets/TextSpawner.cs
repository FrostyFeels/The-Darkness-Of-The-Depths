using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSpawner : MonoBehaviour
{
    public float timer = 0;
    public float wait = 3f;
    public Text text;
    public Color color;

    public void Start()
    {
        text = gameObject.GetComponent<Text>();
    }
    public void Update()
    {
        timer += Time.deltaTime;
        if(timer > wait)
        {
            color = text.color;
            color.a -= 0.5f * Time.deltaTime;
            text.color = color;

            Debug.Log(text.color.a);

            if (text.color.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
