using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    private Vector3 pos;
    void Update()
    {
        Cursor.visible = false;
        pos = Input.mousePosition;
        pos.z = 45;
        pos = Camera.main.ScreenToWorldPoint(pos);

        transform.position = pos;
    }
}
