using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam; 
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawn;
    
    private float timeSinceLastShot;
    
    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }
    
    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {
        if (this.gameObject != null)
        {
            if (!gunData.reloading && this.gameObject.activeSelf)
            {
                StartCoroutine(Reload());
            }
        } else if (this.gameObject == null)
        {
            Debug.Log("Gun does not exist.");
        }
    }
    
    private IEnumerator Reload()
    {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        if(gunData != null)
        {
            gunData.curCapacity = gunData.magSize;
            gunData.reloading = false;
        }
    }

    private bool CanShoot()
    {
        // check if gun is being reloaded, if it matches rpm etc
        float requiredTimeBetweenShots = 1f / (gunData.fireRate / 60f);
        return !gunData.reloading && timeSinceLastShot > requiredTimeBetweenShots;
    }

    public void Shoot()
    {
         //if (gunData.curCapacity > 0 && CanShoot())
        if (CanShoot())
        {
            // check if hitscan or projectile
            if (gunData.useProjectile == false)
            {
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit raycastHit, gunData.maxDistance))
                {
                    IDamageble damageable = raycastHit.transform.GetComponent<IDamageble>();
                    damageable?.Damage(gunData.damage);
                    Debug.Log("Hit: " + raycastHit.transform.name + " for " + gunData.damage + " damage");
                }
            }
            else
            {
              
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);
                Projectiles projectileScript = projectile.GetComponent<Projectiles>();
                if (projectileScript != null)
                {
                    projectileScript.Initialize(cam.forward, gunData.damage, gunData.maxDistance);
                }
                else
                {
                    Debug.LogError("Projectiles script is missing on projectilePrefab.");
                }
            }
            
            // gunData.curCapacity--;
            timeSinceLastShot = 0;
        }
    }

    private void FixedUpdate()
    {
        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance, Color.red);
        PlayerStats.Instance.currentCap = gunData.curCapacity;
        PlayerStats.Instance.magSize = gunData.magSize;
    }
}
