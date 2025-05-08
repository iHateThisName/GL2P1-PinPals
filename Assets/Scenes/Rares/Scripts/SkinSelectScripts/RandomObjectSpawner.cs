using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    //AI GENERATED
    [Header("Spawning Settings")]
    public GameObject[] objectsToSpawn;
    public int numberOfObjects = 10;
    public float spawnRadius = 10f;

    [Header("Spacing Settings")]
    public float minSpacing = 1f;
    public float maxSpacing = 3f;

    private void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        int spawnedCount = 0;
        int attempts = 0;
        Vector3[] positions = new Vector3[numberOfObjects];

        while (spawnedCount < numberOfObjects && attempts < numberOfObjects * 10)
        {
            attempts++;

            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(randomCircle.x, 0, randomCircle.y) + transform.position;

            bool tooClose = false;
            for (int i = 0; i < spawnedCount; i++)
            {
                float dist = Vector3.Distance(positions[i], spawnPosition);
                if (dist < Random.Range(minSpacing, maxSpacing))
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                positions[spawnedCount] = spawnPosition;
                GameObject prefab = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
                Instantiate(prefab, spawnPosition, Quaternion.identity);
                spawnedCount++;
            }
        }

        if (spawnedCount < numberOfObjects)
        {
            Debug.LogWarning("Could not place all objects due to spacing constraints.");
        }
    }
}
