using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Player_WeaponHandler : MonoBehaviour
{
    public Slider Heat;

    //Weapon Manager Variable
    private WeaponManager weaponManager;

    //Weapon data
    private WeaponClasses weaponList;
    private GameObject currentWeapon;
    public Weapons currentWeaponData;
    private Transform bulletSpawn;
    private bool hasWeaponEquipped;
    [SerializeField] private Transform eyeSight;
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private Transform ADSPosition;

    //Weapon index variables
    //private int weaponIndex;
    //private int currentWeaponType;

    private void Start()
    {
        weaponManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<WeaponManager>();
        weaponList = new WeaponClasses(weaponManager.weapons);

        if (!hasWeaponEquipped)
        {
            //If there is a primary weapon available, spawn with that
            foreach (Weapons weapon in weaponList.primaryWeapons)
            {
                //If the weapon in the primary weapons list is available and unlocked, start with that weapon
                if (weapon.weaponPrefab && weapon.bulletPrefab && weapon.unlocked)
                {
                    //Create weapon
                    currentWeapon = Instantiate(weapon.weaponPrefab, weaponPosition, false);
                    //Set weapon data
                    currentWeaponData = weapon;
                    //weaponIndex = Array.IndexOf(weaponList.primaryWeapons, weapon);
                    bulletSpawn = currentWeapon.transform.GetChild(0);

                    hasWeaponEquipped = true;
                    //currentWeaponType = 0;
                    return;
                }
            }
            //If there is a secondary weapon available, spawn with that
            foreach (Weapons weapon in weaponList.secondaryWeapons)
            {
                //If the weapon in the secondary weapons list is available and unlocked, start with that weapon
                if (weapon.weaponPrefab && weapon.bulletPrefab && weapon.unlocked)
                {
                    //Create weapon
                    currentWeapon = Instantiate(weapon.weaponPrefab, weaponPosition, false);
                    //Set weapon data
                    currentWeaponData = weapon;
                    //weaponIndex = Array.IndexOf(weaponList.secondaryWeapons, weapon);
                    bulletSpawn = currentWeapon.transform.GetChild(0);

                    hasWeaponEquipped = true;
                    //currentWeaponType = 0;
                    return;
                }
            }
            //If there is a tertiary weapon available, spawn with that
            foreach (Weapons weapon in weaponList.tertiaryWeapons)
            {
                //If the weapon in the tertiary weapons list is available and unlocked, start with that weapon
                if (weapon.weaponPrefab && weapon.bulletPrefab && weapon.unlocked)
                {
                    //Create weapon
                    currentWeapon = Instantiate(weapon.weaponPrefab, weaponPosition, false);
                    //Set weapon data
                    currentWeaponData = weapon;
                    //weaponIndex = Array.IndexOf(weaponList.tertiaryWeapons, weapon);
                    bulletSpawn = currentWeapon.transform.GetChild(0);

                    hasWeaponEquipped = true;
                    //currentWeaponType = 0;
                    return;
                }
            }
        }
    }

    private void Update()
    {
        //Cooldowns
        weaponManager.Cooldowns(weaponList);

        //Fire weapon
        if (currentWeaponData.fireMode == 1 && !currentWeaponData.isCooling && !currentWeaponData.hasShot)
        {
            if (Input.GetMouseButton(0))
                weaponManager.Fire(bulletSpawn, currentWeapon, currentWeaponData, eyeSight);
        }
        else if (currentWeaponData.fireMode == 2 && !currentWeaponData.isCooling && !currentWeaponData.hasShot)
        {
            if (Input.GetMouseButtonDown(0))
                weaponManager.Fire(bulletSpawn, currentWeapon, currentWeaponData, eyeSight);
        }

        //Aim weapon
        if (Input.GetMouseButton(1))
            weaponManager.Aim(currentWeapon, weaponPosition, ADSPosition, currentWeaponData, true);
        else
            weaponManager.Aim(currentWeapon, weaponPosition, ADSPosition, currentWeaponData, false);

        //Overheat UI
        Heat.value = currentWeaponData.coolingCDTimer / currentWeaponData.coolingCooldown;
    }
}
