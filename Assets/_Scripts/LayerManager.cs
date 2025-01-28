using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public GameObject objectToSpawn; // Reference to the prefab you want to spawn
    public int numberOfObjects = 4; // The number of objects to spawn
    public int startingLayer = 6; // The starting layer index
    public List<Color> colors; // List of colors to assign
    public Camera cameraPrefab;

    [SerializeField] private Transform spawnPoint;

    public void Start()
    {
        SpawnObjects();
    }
    void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Instantiate the GameObject at the specified position and rotation
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);

            // Assign the layer to the spawned object, incrementing the layer index
            int currentLayer = startingLayer + i; // Increment the layer index
            spawnedObject.layer = currentLayer;

            // Assign the color from the list to the spawned object's material
            Renderer objectRenderer = spawnedObject.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                objectRenderer.material.color = colors[i]; // Assign color from the list based on index
            }
            else
            {
                Debug.LogError("No Renderer found on the spawned object!");
            }
            // Spawn and configure a camera for each object
            SpawnCameraForObject(spawnedObject, i);
        }
        if (colors.Count < numberOfObjects)
        {
            Debug.LogError("Not enough colors in the list!");
            return;
        }
    }
    void SpawnCameraForObject(GameObject targetObject, int index)
    {
        // Instantiate the camera for the current object
        Camera newCamera = Instantiate(cameraPrefab, new Vector3(targetObject.transform.position.x, 5, targetObject.transform.position.z - 20), Quaternion.Euler(30, 0, 0));

        // Get the default culling mask of the camera
        int defaultCullingMask = newCamera.cullingMask;

        // Combine the default culling mask with the current layer
        int currentLayer = startingLayer + index;
        newCamera.cullingMask = defaultCullingMask | 1 << currentLayer + 4;  // Preserve default mask and add the specific layer

        // Make the camera focus on the target object
        newCamera.transform.LookAt(targetObject.transform);

        // Optionally set other camera properties like field of view
        newCamera.fieldOfView = 60;
        newCamera.clearFlags = CameraClearFlags.Skybox;
    }
}

