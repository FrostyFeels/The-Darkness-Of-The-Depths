using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeStatsManager : MonoBehaviour
{
    public MeleeEnemyStats stats;
    public Transform player;


    public void Start()
    {
        player = GameObject.Find("SpiderPlayer").transform;
    }
}
