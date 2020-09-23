using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedStatsManager : MonoBehaviour
{
    public RangedEnemyStats stats;
    public Transform player;

    public void Start()
    {
        player = GameObject.Find("SpiderPlayer").transform;
    }
}
