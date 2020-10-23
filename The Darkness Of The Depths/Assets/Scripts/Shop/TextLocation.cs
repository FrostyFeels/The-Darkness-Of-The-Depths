using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLocation : MonoBehaviour
{
    public Text areaUnlock;
    public GameObject area;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnText(string weaponName)
    {
        GameObject unlockText = Instantiate(area, transform);
        areaUnlock = unlockText.GetComponent<Text>();
        areaUnlock.text = "You have unlocked: " + weaponName.ToString();
    }



}
