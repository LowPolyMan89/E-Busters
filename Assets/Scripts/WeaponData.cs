using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public string WeaponID;
    public int DamageMin, DamageNormal, DamageMax;
    public float ReloadTime;
    public float AmmoCount;
    public float AmmoStorage;
    public float AmmoInShoot;
    public float FireRate;
    public float Angle;
    public float Range;
   // public Vector3 DamageProportion;
}
