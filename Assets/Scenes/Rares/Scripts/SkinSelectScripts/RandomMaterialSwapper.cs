using UnityEngine;

public class RandomMaterialSwapper : MonoBehaviour
{
    public Material[] materials;
    public float changeInterval = 1f; // Seconds between material swap

    private Renderer objectRenderer;
    private float timer;

    void Start()
    {
        if (materials.Length == 0)
        {
            enabled = false;
            return;
        }

        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
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
