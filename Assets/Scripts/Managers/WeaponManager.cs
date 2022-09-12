using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManager : MonoBehaviour
{
    //Weapon variables
    public WeaponClasses weapons;

    //Fire weapon function  | TODO: add recoil to currentWeapon
    public void Fire(Transform bulletSpawn, GameObject currentWeapon, Weapons currentWeaponData, Transform eyeSight)
    {
        //Fire a bullet for the current weapon's bullet count amount of times
        for (int i = 0; i < currentWeaponData.bulletCount; i++)
        {
            //Instantiate the bullet prefab & set the bullet's damage
            GameObject bullet = Instantiate(currentWeaponData.bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Bullet>().damage = currentWeaponData.damage;

            //Raycast where the camera is looking
            RaycastHit hit;
            var ray = new Ray(eyeSight.position, eyeSight.forward);
            //Make the bullet look towards the raycast point if it exists, else make the bullet look forward towards the camera
            if (Physics.Raycast(ray, out hit))
                bullet.transform.LookAt(hit.point);
            else
                bullet.transform.rotation = eyeSight.rotation;

            //Get the direction the bullet is facing + a random Vector3 made from the spread radius
            Vector3 dir = bullet.transform.forward + new Vector3(UnityEngine.Random.Range(-currentWeaponData.spreadRadius, currentWeaponData.spreadRadius), UnityEngine.Random.Range(-currentWeaponData.spreadRadius, currentWeaponData.spreadRadius), UnityEngine.Random.Range(-currentWeaponData.spreadRadius, currentWeaponData.spreadRadius));

            //Add force to the bullet by the dir variable * the bullet speed, using ForceMode.Impulse for immediate fixed speed
            bullet.GetComponent<Rigidbody>().AddForce(dir * currentWeaponData.bulletSpeed, ForceMode.Impulse);

            //Set the weapon to hasShot
            currentWeaponData.hasShot = true;

            currentWeaponData.coolingCDTimer += currentWeaponData.coolingCDIncrease;
        }
    }

    //Recoil function
    public void Recoil()
    {
        
    }

    //Aiming function
    public void Aim(GameObject currentWeapon, Transform weaponPosition, Transform adsPosition, Weapons currentWeaponData, bool isAiming)
    {
        if (isAiming)
            currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, adsPosition.localPosition, currentWeaponData.adsSpeed * Time.deltaTime);
        else
            currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, weaponPosition.localPosition, currentWeaponData.adsSpeed * Time.deltaTime);
    }

    //Swap weapons function
    public void SwapWeapon()
    {

    }

    //Cooldown functions
    public void Cooldowns(WeaponClasses weaponList)
    {
        if (weaponList.primaryWeapons != null)
            foreach (Weapons weapon in weaponList.primaryWeapons)
            {
                //If the weapon is cooling and is not recharging, decrease the cooldown timer until it is less than or equal to 0
                if (weapon.isCooling && !weapon.recharging)
                {
                    if (weapon.coolingCDTimer <= 0)
                    {
                        weapon.coolingCDTimer = 0;
                        weapon.isCooling = false;
                    }
                    else
                        weapon.coolingCDTimer -= Time.deltaTime;
                }
                //If the weapon is recharging, drecrease the cooldown timer faster until it is less than or equal to 0
                else if (weapon.isCooling && weapon.recharging)
                {
                    if (weapon.coolingCDTimer <= 0)
                    {
                        weapon.coolingCDTimer = 0;
                        weapon.isCooling = false;
                    }
                    else
                        weapon.coolingCDTimer -= Time.deltaTime * weapon.rechargingSpeed;
                }
                //If the weapon is not cooling but its cooldownCDTimer is greater tahn 0, decrease teh cooldown timer by Time.deltaTime
                else if (weapon.coolingCDTimer > 0)
                    weapon.coolingCDTimer -= Time.deltaTime;
                //If the weapon's coolingCDTimer is greater than or equal to the coolingCooldown, set the weapon cooling to true
                if (weapon.coolingCDTimer >= weapon.coolingCooldown)
                    weapon.isCooling = true;

                //If the weapon has shot, increase the rate of fire cooldown until it is greater tahn or equal to the cooldown length
                if (weapon.hasShot)
                {
                    if (weapon.rateOfFireCDTimer >= weapon.rateOfFire)
                    {
                        weapon.rateOfFireCDTimer = 0;
                        weapon.hasShot = false;
                    }
                    else
                        weapon.rateOfFireCDTimer += Time.deltaTime;
                }
            }
        if (weaponList.secondaryWeapons != null)
            foreach (Weapons weapon in weaponList.secondaryWeapons)
            {
                //If the weapon is cooling and is not recharging, decrease the cooldown timer until it is less than or equal to 0
                if (weapon.isCooling && !weapon.recharging)
                {
                    if (weapon.coolingCDTimer <= 0)
                    {
                        weapon.coolingCDTimer = 0;
                        weapon.isCooling = false;
                    }
                    else
                        weapon.coolingCDTimer -= Time.deltaTime;
                }
                //If the weapon is recharging, drecrease the cooldown timer faster until it is less than or equal to 0
                else if (weapon.isCooling && weapon.recharging)
                {
                    if (weapon.coolingCDTimer <= 0)
                    {
                        weapon.coolingCDTimer = 0;
                        weapon.isCooling = false;
                    }
                    else
                        weapon.coolingCDTimer -= Time.deltaTime * weapon.rechargingSpeed;
                }
                //If the weapon is not cooling but its cooldownCDTimer is greater tahn 0, decrease teh cooldown timer by Time.deltaTime
                else if (weapon.coolingCDTimer > 0)
                    weapon.coolingCDTimer -= Time.deltaTime;
                //If the weapon's coolingCDTimer is greater than or equal to the coolingCooldown, set the weapon cooling to true
                if (weapon.coolingCDTimer >= weapon.coolingCooldown)
                    weapon.isCooling = true;

                //If the weapon has shot, increase the rate of fire cooldown until it is greater tahn or equal to the cooldown length
                if (weapon.hasShot)
                {
                    if (weapon.rateOfFireCDTimer >= weapon.rateOfFire)
                    {
                        weapon.rateOfFireCDTimer = 0;
                        weapon.hasShot = false;
                    }
                    else
                        weapon.rateOfFireCDTimer += Time.deltaTime;
                }
            }
        if (weaponList.tertiaryWeapons != null)
            foreach (Weapons weapon in weaponList.tertiaryWeapons)
            {
                //If the weapon is cooling and is not recharging, decrease the cooldown timer until it is less than or equal to 0
                if (weapon.isCooling && !weapon.recharging)
                {
                    if (weapon.coolingCDTimer <= 0)
                    {
                        weapon.coolingCDTimer = 0;
                        weapon.isCooling = false;
                    }
                    else
                        weapon.coolingCDTimer -= Time.deltaTime;
                }
                //If the weapon is recharging, drecrease the cooldown timer faster until it is less than or equal to 0
                else if (weapon.isCooling && weapon.recharging)
                {
                    if (weapon.coolingCDTimer <= 0)
                    {
                        weapon.coolingCDTimer = 0;
                        weapon.isCooling = false;
                    }
                    else
                        weapon.coolingCDTimer -= Time.deltaTime * weapon.rechargingSpeed;
                }
                //If the weapon is not cooling but its cooldownCDTimer is greater tahn 0, decrease teh cooldown timer by Time.deltaTime
                else if (weapon.coolingCDTimer > 0)
                    weapon.coolingCDTimer -= Time.deltaTime;
                //If the weapon's coolingCDTimer is greater than or equal to the coolingCooldown, set the weapon cooling to true
                if (weapon.coolingCDTimer >= weapon.coolingCooldown)
                    weapon.isCooling = true;

                //If the weapon has shot, increase the rate of fire cooldown until it is greater tahn or equal to the cooldown length
                if (weapon.hasShot)
                {
                    if (weapon.rateOfFireCDTimer >= weapon.rateOfFire)
                    {
                        weapon.rateOfFireCDTimer = 0;
                        weapon.hasShot = false;
                    }
                    else
                        weapon.rateOfFireCDTimer += Time.deltaTime;
                }
            }
    }
}

//Weapon classes
[Serializable]
public class WeaponClasses
{
    public Weapons[] primaryWeapons;
    public Weapons[] secondaryWeapons;
    public Weapons[] tertiaryWeapons;

    public WeaponClasses(WeaponClasses other)
    {
        primaryWeapons = new Weapons[other.primaryWeapons.Length];
        secondaryWeapons = new Weapons[other.secondaryWeapons.Length];
        tertiaryWeapons = new Weapons[other.tertiaryWeapons.Length];
        for (int i = 0; i < other.primaryWeapons.Length; i++)
        {
            this.primaryWeapons[i] = new Weapons(other.primaryWeapons[i]);
        }
        for (int i = 0; i < other.secondaryWeapons.Length; i++)
        {
            this.secondaryWeapons[i] = new Weapons(other.secondaryWeapons[i]);
        }
        for (int i = 0; i < other.tertiaryWeapons.Length; i++)
        {
            this.tertiaryWeapons[i] = new Weapons(other.tertiaryWeapons[i]);
        }
    }
}
//Weapon data
[Serializable]
public class Weapons
{
    //Weapon information
    [Header("Weapon Information")]
    public string weaponName;
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;
    public bool unlocked;

    //Weapon specs
    [Header("Weapon Specs")]
    [Range(1,2)]
    [Tooltip("1 Auto, 2 Semi-Auto")]
    public int fireMode = 1;
    public float damage;
    public float spreadRadius;
    public int bulletCount;
    public float bulletSpeed;
    public float rateOfFire;
    public float recoilAmount;
    public float adsSpeed;

    //Rate of fire variables
    public bool hasShot;
    public float rateOfFireCDTimer;

    //Cooling variables
    public bool isCooling;
    public bool recharging;
    [Header("Cooling Variables")]
    public float coolingCooldown;
    public float coolingCDTimer;
    public float coolingCDIncrease;
    public float rechargingSpeed = 1;

    public Weapons(Weapons other)
    {
        this.weaponName = other.weaponName;
        this.weaponPrefab = other.weaponPrefab;
        this.bulletPrefab = other.bulletPrefab;
        this.unlocked = other.unlocked;

        this.fireMode = other.fireMode;
        this.damage = other.damage;
        this.spreadRadius = other.spreadRadius;
        this.bulletCount = other.bulletCount;
        this.bulletSpeed = other.bulletSpeed;
        this.rateOfFire = other.rateOfFire;
        this.recoilAmount = other.recoilAmount;
        this.adsSpeed = other.adsSpeed;

        this.hasShot = other.hasShot;
        this.rateOfFireCDTimer = other.rateOfFireCDTimer;

        this.isCooling = other.isCooling;
        this.recharging = other.recharging;
        this.coolingCooldown = other.coolingCooldown;
        this.coolingCDTimer = other.coolingCDTimer;
        this.rechargingSpeed = other.rechargingSpeed;
    }
}