using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Enemy", menuName = "Enemy/Ranged")]
public class RangedEnemyStats : EnemyStats
{
    public float AttackSpeed;
    public float reloadSpeed;
    public float minSpread;
    public float maxSpread;
    public float BulletSpeed;
    public int NumberOfShots;
    public int ammo;
}
