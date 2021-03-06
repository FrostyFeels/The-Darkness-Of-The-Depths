﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Movement playermovent;

  
    public float health;
    public float regen;
    public int armor;
    public float maxHealth;

    private void Start()
    {
        maxHealth = health;
        health = StaticManager.playerHealth;
        playermovent = gameObject.GetComponent<Movement>();  
    }

    public void TakeDamage(int damage)
    {
        health -= (damage - armor);
        StaticManager.playerHealth = health;
    }

    public void GetHP(int healthAmount)
    {
        if(health + healthAmount <= maxHealth)
        {
            health += healthAmount;
            StaticManager.playerHealth = health;
        }
        else
        {
            health = maxHealth;
            StaticManager.playerHealth = health;
        }
        
    }

    public void Injured()
    {
        playermovent.maxSpeed = 8;
    }

    public void NotInjured()
    {
        playermovent.maxSpeed = 17;
    }



    public void FixedUpdate()
    {
        if(health <= 150)
        {
            health += regen;
        }
        
    }


    public void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }


}
