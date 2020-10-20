using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveArea : MonoBehaviour
{
    public string areaName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StaticManager.SetArea(areaName);           
        }
    }
}
