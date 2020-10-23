using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTutorial : MonoBehaviour
{
    public int textnumber = 0;
    public Text[] texts;

    public Color color;
    public bool fadeText;

    public void Start()
    {
        spawnText();
    }

    public void Update()
    {
        if(fadeText)
        {
            color = texts[textnumber].color;
            color.a -= 0.5f * Time.deltaTime;
            texts[textnumber].color = color;
            if (texts[textnumber].color.a < 0)
            {
                texts[textnumber].enabled = false;
                fadeText = false;
            }
        }
    }


    public void spawnText()
    {
        texts[textnumber].enabled = true;        
        StartCoroutine(startFading());

        if(textnumber > 0)
        {
            texts[textnumber - 1].enabled = false;
        }
    }

   IEnumerator startFading()
    {
        yield return new  WaitForSeconds(1.5f);
        fadeText = true;
    }
}
