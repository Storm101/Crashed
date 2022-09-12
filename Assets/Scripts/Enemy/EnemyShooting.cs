using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {
    //Weapon Manager Variable
    private WeaponManager weaponManager;

    //Weapon data
    private WeaponClasses weaponList;
    private GameObject currentWeapon;
    public Weapons currentWeaponData;
    public Transform BulletSpawn;
    private Transform bulletSpawn;
    private bool hasWeaponEquipped;

    public bool Shoot;
    [SerializeField] private Transform eyeSight;

    //Weapon index variables
    //private int weaponIndex;
    //private int currentWeaponType;

    private void Start() {
        weaponManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<WeaponManager>();
        weaponList = weaponManager.weapons;

        if (!hasWeaponEquipped) {
            //If there is a primary weapon available, spawn with that
            foreach (Weapons weapon in weaponList.primaryWeapons) {
                //If the weapon in the primary weapons list is available and unlocked, start with that weapon
                if (weapon.weaponPrefab && weapon.bulletPrefab && weapon.unlocked) {
                    //Create weapon
                    //Set weapon data
                    currentWeaponData = weapon;
                    //weaponIndex = Array.IndexOf(weaponList.primaryWeapons, weapon);
                    hasWeaponEquipped = true;
                    //currentWeaponType = 0;
                    bulletSpawn = BulletSpawn;
                    return;
                }
            }
        }
    }

    private void Update() {
        //Cooldowns
        weaponManager.Cooldowns(weaponList);

        //Fire weapon
        if (currentWeaponData.fireMode == 1 && !currentWeaponData.isCooling && !currentWeaponData.hasShot) {
            if (Shoot)
                weaponManager.Fire(bulletSpawn, null, currentWeaponData, eyeSight);
        }
        else if (currentWeaponData.fireMode == 2 && !currentWeaponData.isCooling && !currentWeaponData.hasShot) {
            if (Shoot)
                weaponManager.Fire(bulletSpawn, null, currentWeaponData, eyeSight);
        }
    }
}
