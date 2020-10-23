using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossHealthBar : MonoBehaviour
{
    public  Slider slider;
    public BossHealth health;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

        slider.maxValue = health.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health.health;
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
