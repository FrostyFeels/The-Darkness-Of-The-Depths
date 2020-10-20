using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    public PlayerHealth health;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = health.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health.health;
    }
}
