using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    EnemyMovement movement;
    EnemyFire fire;
    

    public enum Ai
    {
        Walking,
        Running,
        Shooting,
        Reloading,
        TakingDamage,
        Dead
    }

    public Ai ai;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<EnemyMovement>();
        fire = GetComponent<EnemyFire>();
        ai = Ai.Walking;
    }

    // Update is called once per frame
    void Update()
    {
        switch(ai)
        {
            case Ai.Walking:
                movement.speed = movement.averagespeed;
                movement.enabled = true;
                return;
            case Ai.Running:
                movement.speed = movement.averagespeed * 2;
                return;
            case Ai.Shooting:
                
                return;
            case Ai.Reloading:
                return;
            case Ai.TakingDamage:
                return;
            case Ai.Dead:
                Destroy(gameObject);
                return;
        }




    }
}
