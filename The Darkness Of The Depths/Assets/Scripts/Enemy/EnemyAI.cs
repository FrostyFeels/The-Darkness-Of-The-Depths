using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool ranged;
    public bool melee;
    public bool sniper;
    public bool tank;
    public bool fixWeapon;
    public EnemyMovement movement;
    public RangedAttack rangedAttack;
    public MeleeAttack meleeAttack;
    public GameObject lights; 
    
    

    public enum Ai
    { 
        searching,
        chasing,
        Attacking,       
        Reloading,
        aiming,
        TakingDamage, 
        Dead
    }

    public Ai ai;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<EnemyMovement>();
        ai = Ai.searching;
    }

    // Update is called once per frame
    void Update()
    {
        switch(ai)
        {
            case Ai.searching:
                movement.speed = movement.averagespeed;
                movement.enabled = true;

                if(ranged)
                {
                    if(!fixWeapon)
                    {
                        rangedAttack.resetWeapon();
                        fixWeapon = true;
                    }

                    rangedAttack.enabled = false;

                }
                if (melee)
                {
                    meleeAttack.enabled = false;
                }

                return;
            case Ai.chasing:
                if(ranged)
                {
                    movement.speed = 0;
                }
                return;
            case Ai.aiming:
                rangedAttack.enabled = true;
                movement.speed = 0;
                fixWeapon = false;
                return;
            case Ai.Attacking:

                if(melee)
                {
                    meleeAttack.enabled = true;
                    meleeAttack.attacking = true;
                }
                movement.speed = 0;
                return;

            case Ai.Reloading:
                if(ranged)
                {
                    movement.speed = 0;
                }
                if(melee)
                {
                    movement.speed = movement.averagespeed / 2;
                }   
                return;

            case Ai.TakingDamage:
                return;

            case Ai.Dead:               
                Destroy(gameObject);
                return;
        }




    }
}
