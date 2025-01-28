using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public GameObject spawnPinball;
    public int numberOfObjects = 4;
    public int startingLayer = 6; // Starting layer
    public List<Color> colors;
    public Camera cameraPrefab;

    [SerializeField] private Transform spawnPoint;

    public void Start()
    {
        SpawnPinball();
    }
    void SpawnPinball()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
         
            GameObject spawnedObject = Instantiate(spawnPinball, spawnPoint.position, Quaternion.identity);

            // Assigning the layer +1
            int currentLayer = startingLayer + i;
            spawnedObject.layer = currentLayer;

            // Color
            Renderer objectRenderer = spawnedObject.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                objectRenderer.material.color = colors[i]; // Assigning from the list
            }
            else
            {
                Debug.LogError("No Renderer found on the spawned object!");
            }
            // Spawns a camera for each object
            SpawnCameraForPinball(spawnedObject, i);
        }
        if (colors.Count < numberOfObjects)
        {
            Debug.LogError("Not enough colors in the list!");
            return;
        }
    }
    void SpawnCameraForPinball(GameObject targetObject, int index)
    {
        // Instantiate the camera for the current object
        Camera newCamera = Instantiate(cameraPrefab, new Vector3(targetObject.transform.position.x, 5, targetObject.transform.position.z - 20), Quaternion.Euler(30, 0, 0));

        // Get the default culling mask of the camera
        int defaultCullingMask = newCamera.cullingMask;

        // Combine the default culling mask with the current layer + 4(Number of max players) to get to the flipper layers
        int currentLayer = startingLayer + index;
        newCamera.cullingMask = defaultCullingMask | 1 << currentLayer + 4;  // Defaultmasks + flippers

        // Camera settings
        newCamera.transform.LookAt(targetObject.transform);

        
        newCamera.fieldOfView = 60;
        newCamera.clearFlags = CameraClearFlags.Skybox;
    }
}

