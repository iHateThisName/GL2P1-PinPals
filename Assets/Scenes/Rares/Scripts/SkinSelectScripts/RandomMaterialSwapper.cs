using UnityEngine;

public class RandomMaterialSwapper : MonoBehaviour
{
    [Header("Material Options")]
    public Material[] materials;

    [Header("Timing")]
    public float changeInterval = 1f; // Seconds between material swaps

    private Renderer objectRenderer;
    private float timer;

    void Start()
    {
        if (materials.Length == 0)
        {
            Debug.LogWarning("No materials assigned to RandomMaterialSwapper on " + gameObject.name);
            enabled = false;
            return;
        }

        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogWarning("No Renderer found on " + gameObject.name);
            enabled = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0f;
            Material newMat = materials[Random.Range(0, materials.Length)];
            objectRenderer.material = newMat;
        }
    }
}
