using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Manager", menuName = "ScriptableObjects/WeaponManager", order = 1)]
public class WeaponManager : ScriptableObject
{
    //Weapon variables
    [SerializeField] private WeaponClasses weapons;
    private int weaponIndex;
    private Weapons currentWeaponData;
    private GameObject currentWeapon;
    private Transform bulletSpawn;

    //Fire weapon function
    public void Fire()
    {
        //Fire a bullet for the current weapon's bullet count amount of times
        for (int i = 0; i < currentWeaponData.bulletCount; i++)
        {
            //Instantiate the bullet prefab & set the bullet's damage
            GameObject bullet = Instantiate(currentWeaponData.bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Bullet>().damage = currentWeaponData.damage;

            //Raycast where the camera is looking
            RaycastHit hit;
            var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            //Make the bullet look towards the raycast point if it exists, else make the bullet look forward towards the camera
            if (Physics.Raycast(ray, out hit))
                bullet.transform.LookAt(hit.point);
            else
                bullet.transform.rotation = Camera.main.transform.rotation;

            //Get the direction the bullet is facing + a random Vector3 made from the spread radius
            Vector3 dir = bullet.transform.forward + new Vector3(UnityEngine.Random.Range(-currentWeaponData.spreadRadius, currentWeaponData.spreadRadius), UnityEngine.Random.Range(-currentWeaponData.spreadRadius, currentWeaponData.spreadRadius), UnityEngine.Random.Range(-currentWeaponData.spreadRadius, currentWeaponData.spreadRadius));

            //Add force to the bullet by the dir variable * the bullet speed, using ForceMode.Impulse for immediate fixed speed
            bullet.GetComponent<Rigidbody>().AddForce(dir * currentWeaponData.bulletSpeed, ForceMode.Impulse);

            //Set the weapon to hasShot
            currentWeaponData.hasShot = true;

            currentWeaponData.coolingCDTimer += currentWeaponData.coolingCDIncrease;
        }
    }

    //Cooldown functions
    public void Cooldowns()
    {
        foreach (Weapons weapon in weapons.primaryWeapons)
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
            else if (currentWeaponData.coolingCDTimer > 0)
                currentWeaponData.coolingCDTimer -= Time.deltaTime;
            //If the weapon's coolingCDTimer is greater than or equal to the coolingCooldown, set the weapon cooling to true
            if (currentWeaponData.coolingCDTimer >= currentWeaponData.coolingCooldown)
                currentWeaponData.isCooling = true;

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
    [Range(1,3)]
    [Tooltip("1 Auto, 2 Semi-Auto, 3 Manual")]
    public int fireMode;
    public float damage;
    public float spreadRadius;
    public int bulletCount;
    public float bulletSpeed;
    public float rateOfFire;
    public float recoilAmount;

    //Rate of fire variables
    [HideInInspector] public bool hasShot;
    [HideInInspector] public float rateOfFireCDTimer;

    //Cooling variables
    [HideInInspector] public bool isCooling;
    [HideInInspector] public bool recharging;
    [Header("Cooling Variables")]
    public float coolingCooldown;
    public float coolingCDTimer;
    public float coolingCDIncrease;
    public float rechargingSpeed;
}