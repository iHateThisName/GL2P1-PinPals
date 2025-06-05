using UnityEngine;
using System.Collections;
//an AKW Script
public class LauncerThingieScript : MonoBehaviour
{
    public Transform cannonBase;
    public Transform cannonExit;

    public GameObject projectilePrefab;

    public float projectileSpeed = 10f;

    public float minFireInterval = 1f;
    public float maxFireInterval = 10f;

    public float fireCooldown = 5f;

    private float lastFireTime = 0f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            FireCannon();
            nextFireTime = Time.time + Random.Range(minFireInterval, maxFireInterval);
        }
    }

    void FireCannon()
    {
        if (Time.time >= lastFireTime + fireCooldown)
        {
            Vector3 fireDirection = (cannonExit.position - cannonBase.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, cannonBase.position, Quaternion.identity);

            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            if (projectileRb != null)
            {
                Destroy(projectileRb);
            }

            projectile.transform.rotation = Quaternion.LookRotation(fireDirection);

            StartCoroutine(MoveProjectile(projectile, fireDirection));

            Destroy(projectile, 15f);

            lastFireTime = Time.time;
        }
    }

    private IEnumerator MoveProjectile(GameObject projectile, Vector3 direction)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 5f) 
        {
            projectile.transform.position += direction * projectileSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}