using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //Weapon variables
    [SerializeField] private WeaponClasses weapons;
}

//Weapon Classes
[Serializable]
public class WeaponClasses
{
    public Weapons[] primaryWeapons;
    public Weapons[] secondaryWeapons;
    public Weapons[] tertiaryWeapons;
}
//Weapon Data
[Serializable]
public class Weapons
{
    //Weapon Information
    [Header("Weapon Information")]
    public string weaponName;
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;
    public bool unlocked;

    //Weapon Specs
    [Header("Weapon Specs")]
    [Range(1,3)]
    public int fireMode;
    public float damage;
    public float spreadRadius;
    public int bulletCount;
    public float bulletSpeed;
    public float rateOfFire;

    //Rate of Fire Variables
    [HideInInspector] public bool hasShot;
    [HideInInspector] public float rateOfFireCooldown;

    //Cooling Variables
    [HideInInspector] public bool isCooling;
    [Header("Cooling Variables")]
    public float coolingCooldown;
    public float coolingCDTimer;

    //Overheat Variables
    [HideInInspector] public bool hasOverheated;
    [Header("Overheating Variables")]
    public float overheatCooldown;
    public float overheatCDTimer;
}