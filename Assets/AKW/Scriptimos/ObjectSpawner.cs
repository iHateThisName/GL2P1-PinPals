using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 10f;
    public float objectLifetime = 10f;

    private float nextSpawnTime;

    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            GameObject spawnedObject = Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
            Destroy(spawnedObject, objectLifetime);
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }
}
