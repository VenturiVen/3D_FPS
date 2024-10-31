 using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName="Weapon", menuName="Lazer Staff")]
public class GunData : ScriptableObject
    
{
    [Header("Name of Gun")]
    public new string name;

    [Header("Gun Statistics")]
    public int curCapacity;
    public int magSize;
    public float fireRate;
    public float damage;
    public  float maxDistance;



    [Header("Reloading")]
    public float reloadTime;
    public bool reloading;

    
    // public float damageDropoff?
    // public float randomness (for recoil?)

}
