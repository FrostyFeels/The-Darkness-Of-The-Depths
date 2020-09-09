using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    EnemyMovement movement;
    public EnemyFire fire;
    public bool aiming, shooting = false;
    public LineRenderer line;
    

    public enum Ai
    {
   
        Running,
        Shooting,
        Reloading,
        TakingDamage,
        aiming,
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
                fire.enabled = false;
                line.enabled = false;
     
                return;
            case Ai.Shooting:
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
            case Ai.aiming:
                movement.speed = movement.averagespeed / 2;
                fire.enabled = true;
                line.enabled = true;
                return;
        }




    }
}
