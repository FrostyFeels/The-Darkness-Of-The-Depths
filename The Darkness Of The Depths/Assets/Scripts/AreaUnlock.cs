using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaUnlock : MonoBehaviour
{

    public string doorName;

    // Update is called once per frame
    void Update()
    {
        if (StaticManager.SelectDoorType(doorName) && gameObject.layer == LayerMask.NameToLayer(doorName))
        {
            gameObject.active = false;
        }

    }
}
