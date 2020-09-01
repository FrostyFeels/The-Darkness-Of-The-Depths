using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponStats : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    public float dmg;
    public float knockBackForce;
    public float ArmorPenentration;
    public float range;
}
