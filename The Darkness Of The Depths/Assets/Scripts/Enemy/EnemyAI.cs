using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool ranged;
    public bool melee;
    public EnemyMovement movement;
    public RangedAttack rangedAttack;
    public MeleeAttack meleeAttack;
    
    

    public enum Ai
    { 
        Running,
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
        ai = Ai.Running;
    }

    // Update is called once per frame
    void Update()
    {
        switch(ai)
        {
            case Ai.Running:
                movement.speed = movement.averagespeed;
                movement.enabled = true;
                if(ranged)
                {
                    rangedAttack.enabled = false;
                }
                if(melee)
                {
                    meleeAttack.enabled = false;
                }
                return;
            case Ai.aiming:
                rangedAttack.enabled = true;                          
                movement.speed = movement.averagespeed / 2;               
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
                movement.speed = movement.averagespeed / 2;
                return;
            case Ai.TakingDamage:                
                return;
            case Ai.Dead:
               
                Destroy(gameObject);
                return;
        }




    }
}
