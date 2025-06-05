using System.Collections.Generic;
using UnityEngine;
//an AKW Script
//assisted by Inga
public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> ObjectsToSpawn;
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
            int randomNumber = Random.Range(0, ObjectsToSpawn.Count);
            GameObject spawnedObject = Instantiate(ObjectsToSpawn[randomNumber], transform.position, Quaternion.identity);
            Destroy(spawnedObject, objectLifetime);
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }
}
