using System;
using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;

    private float timeSinceLastShot;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.curCapacity = gunData.magSize;
        gunData.reloading = false;
    }

    private bool CanShoot()
    {
        float requiredTimeBetweenShots = 1f / (gunData.fireRate / 60f);
        return !gunData.reloading && timeSinceLastShot > requiredTimeBetweenShots;
    }

    public void Shoot()
    {
        if (gunData.curCapacity > 0 && CanShoot())
        {
            if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit raycastHit, gunData.maxDistance))
            {
                IDamageble damageable = raycastHit.transform.GetComponent<IDamageble>();
                damageable?.Damage(gunData.damage);
                Debug.Log("Hit: " + raycastHit.transform.name +" for " + gunData.damage + " damage");

            }
            gunData.curCapacity--;
            timeSinceLastShot = 0;
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(muzzle.position, muzzle.forward * gunData.maxDistance, Color.red); //show ray + gun range
    }


}