using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player_WeaponHandler : MonoBehaviour
{
    //Weapon Manager Variable
    public WeaponManager weaponManager;

    public Transform weaponSpawn;
    public Transform bulletSpawn;

    private GameObject currentWeapon;
    public Weapons currentWeaponData;

    private bool foundWeapon;
    private string currentWeaponType;

    private void Start()
    {
        if (!foundWeapon)
        {
            //If there is a primary weapon available, spawn with that
            foreach (Weapons weapon in weaponManager.weapons.primaryWeapons)
            {
                //If the weapon in the primary weapons list is available and unlocked, start with that weapon
                if (weapon.weaponPrefab && weapon.bulletPrefab && weapon.unlocked)
                {
                    //Create weapon
                    //weaponManager.currentWeapon = Instantiate(weapon.weaponPrefab, weaponManager.weaponSpawn, false);
                    //Set weapon data
                    //weaponManager.currentWeaponData = weapon;
                    weaponManager.weaponIndex = Array.IndexOf(weaponManager.weapons.primaryWeapons, weapon);
                    //weaponManager.bulletSpawn = weaponManager.currentWeapon.transform.GetChild(0);

                    foundWeapon = true;
                    currentWeaponType = "primary";
                    return;
                }
            }
            //If there is a secondary weapon available, spawn with that
            foreach (Weapons weapon in weaponManager.weapons.secondaryWeapons)
            {
                //If the weapon in the secondary weapons list is available and unlocked, start with that weapon
                if (weapon.weaponPrefab && weapon.bulletPrefab && weapon.unlocked)
                {
                    //Create weapon
                    //weaponManager.currentWeapon = Instantiate(weapon.weaponPrefab, weaponManager.weaponSpawn, false);
                    //Set weapon data
                    //weaponManager.currentWeaponData = weapon;
                    weaponManager.weaponIndex = Array.IndexOf(weaponManager.weapons.secondaryWeapons, weapon);
                    //weaponManager.bulletSpawn = weaponManager.currentWeapon.transform.GetChild(0);

                    foundWeapon = true;
                    currentWeaponType = "secondary";
                    return;
                }
            }
            //If there is a tertiary weapon available, spawn with that
            foreach (Weapons weapon in weaponManager.weapons.tertiaryWeapons)
            {
                //If the weapon in the tertiary weapons list is available and unlocked, start with that weapon
                if (weapon.weaponPrefab && weapon.bulletPrefab && weapon.unlocked)
                {
                    //Create weapon
                    //weaponManager.currentWeapon = Instantiate(weapon.weaponPrefab, weaponManager.weaponSpawn, false);
                    //Set weapon data
                    //weaponManager.currentWeaponData = weapon;
                    weaponManager.weaponIndex = Array.IndexOf(weaponManager.weapons.tertiaryWeapons, weapon);
                    //weaponManager.bulletSpawn = weaponManager.currentWeapon.transform.GetChild(0);

                    foundWeapon = true;
                    currentWeaponType = "tertiary";
                    return;
                }
            }
        }
    }

    private void Update()
    {
        //Fire weapon
        /*if (weaponManager.currentWeaponData.fireMode == 1 && !weaponManager.currentWeaponData.isCooling && !weaponManager.currentWeaponData.hasShot)
        {
            if (currentWeaponType == "primary")
                if (Input.GetMouseButton(0) && !weaponManager.weapons.primaryWeapons[weaponManager.weaponIndex].isCooling)
                    weaponManager.Fire();
                else if (currentWeaponType == "secondary")
                    if (Input.GetMouseButton(0) && !weaponManager.weapons.primaryWeapons[weaponManager.weaponIndex].isCooling)
                        weaponManager.Fire();
                    else if (currentWeaponType == "tertiary")
                        if (Input.GetMouseButton(0) && !weaponManager.weapons.primaryWeapons[weaponManager.weaponIndex].isCooling)
                            weaponManager.Fire();
        }
        else if (!weaponManager.currentWeaponData.isCooling && !weaponManager.currentWeaponData.hasShot)
        {
            if (currentWeaponType == "primary")
                if (Input.GetMouseButtonDown(0) && !weaponManager.weapons.primaryWeapons[weaponManager.weaponIndex].isCooling)
                    weaponManager.Fire();
                else if (currentWeaponType == "secondary")
                    if (Input.GetMouseButtonDown(0) && !weaponManager.weapons.primaryWeapons[weaponManager.weaponIndex].isCooling)
                        weaponManager.Fire();
                    else if (currentWeaponType == "tertiary")
                        if (Input.GetMouseButtonDown(0) && !weaponManager.weapons.primaryWeapons[weaponManager.weaponIndex].isCooling)
                            weaponManager.Fire();
        }*/
    }
}
