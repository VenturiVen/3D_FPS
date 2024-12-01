using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script was made with the help of this YouTube video:
// https://www.youtube.com/watch?v=kXbQMhwj5Uc
 
[CreateAssetMenu(fileName="Weapon", menuName="Lazer Staff")]
public class GunData : ScriptableObject
{
    [Header("Name of Gun")]
    public new string name;

    public bool useProjectile;
    public bool useHitscan;

    [Header("Gun Statistics")]
    public int curCapacity;
    public int magSize;
    public float fireRate;
    public float damage;
    public float maxDistance;
    public float launchForce;
   
    [Header("Reloading")]
    public float reloadTime;
    public bool reloading;

    
    // public float damageDropoff?
    // public float randomness (for recoil?)

    public int getCurrentCapacity()
    {
        return curCapacity;
    }

    public int getMagSize()
    {
        return magSize;
    }
}
