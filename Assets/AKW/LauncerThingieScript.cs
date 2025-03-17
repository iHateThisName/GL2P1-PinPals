using UnityEngine;
using System.Collections;

public class LauncerThingieScript : MonoBehaviour
{
    // References to the Base and Exit
    public Transform cannonBase; // The base of the cannon
    public Transform cannonExit; // The exit of the cannon (muzzle)

    // The prefab to be fired from the cannon
    public GameObject projectilePrefab;

    // The speed of the projectile (can be reconfigured)
    public float projectileSpeed = 10f;

    // The minimum and maximum time between shots
    public float minFireInterval = 1f;
    public float maxFireInterval = 10f;

    // The cooldown time between consecutive shots (in seconds)
    public float fireCooldown = 5f;

    private float lastFireTime = 0f;
    private float nextFireTime = 0f;

    // Update is called once per frame
    void Update()
    {
        // If enough time has passed, try to fire a shot
        if (Time.time >= nextFireTime)
        {
            FireCannon();
            // Schedule the next shot with random intervals
            nextFireTime = Time.time + Random.Range(minFireInterval, maxFireInterval);
        }
    }

    // Function to fire the cannon
    void FireCannon()
    {
        // Only fire if the cooldown period has passed
        if (Time.time >= lastFireTime + fireCooldown)
        {
            // Calculate the direction from the cannon base to the cannon exit
            Vector3 fireDirection = (cannonExit.position - cannonBase.position).normalized;

            // Instantiate the projectile at the cannon base position
            GameObject projectile = Instantiate(projectilePrefab, cannonBase.position, Quaternion.identity);

            // Remove Rigidbody if it exists (no Rigidbody for the projectile)
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            if (projectileRb != null)
            {
                Destroy(projectileRb); // Remove Rigidbody if it exists
            }

            // Initialize the projectile's velocity manually by updating its position each frame
            StartCoroutine(MoveProjectile(projectile, fireDirection));

            // Destroy the projectile after 15 seconds (changed from 5 seconds)
            Destroy(projectile, 15f); // 15 seconds before the projectile is destroyed

            // Update the last fire time
            lastFireTime = Time.time;
        }
    }

    // Coroutine to move the projectile over time
    private IEnumerator MoveProjectile(GameObject projectile, Vector3 direction)
    {
        float elapsedTime = 0f;

        // Move the projectile in the direction with the specified speed over time
        while (elapsedTime < 5f) // Move for 5 seconds
        {
            projectile.transform.position += direction * projectileSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }
}