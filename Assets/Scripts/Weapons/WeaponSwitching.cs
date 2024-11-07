using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [Header("References:")]
    [SerializeField] private Transform[] weapons;
    
    [Header("Binds:")]
    [SerializeField] private KeyCode[] binds;
    
    [Header("Settings:")]
    [SerializeField] private float switchTime;

    private int selectedWeapon = 0; // start with default weapon
    private float timeSinceSwitching;

    private void Start()
    {
        SetWeapons();
        Select(selectedWeapon);
        timeSinceSwitching = switchTime; // let player swap gun immediately at the start
    }
    
    private void Update()
    {
        timeSinceSwitching += Time.deltaTime;
        
        int previousWeapon = selectedWeapon;
        for (int i = 0; i < binds.Length; i++)
        {
            if (Input.GetKeyDown(binds[i]) && timeSinceSwitching >= switchTime)
            {
                selectedWeapon = i;
            }
        }

        // if selected weapon changed, change weapon
        if (previousWeapon != selectedWeapon)
        {
            Select(selectedWeapon);
        } 
    }

    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount]; // count all children as weapons
        for (int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i);
        }

        // set the binds on the index for each weapon
        if (binds == null || binds.Length != weapons.Length)
        {
            binds = new KeyCode[weapons.Length];
        }
    }

    private void Select(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }
        timeSinceSwitching = 0f;
        Debug.Log($"Weapon {weaponIndex} has been selected.");
    }
}
