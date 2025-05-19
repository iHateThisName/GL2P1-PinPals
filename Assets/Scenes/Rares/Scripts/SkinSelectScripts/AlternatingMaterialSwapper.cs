using UnityEngine;

public class AlternatingMaterialSwapper : MonoBehaviour
{
    public Material[] materials; // Put at least 2
    public float changeInterval = 1f;

    private Renderer objectRenderer;
    private int currentIndex = 0;
    private float timer;

    void Start()
    {
        if (materials.Length != 2)
        {
            enabled = false;
            return;
        }

        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogWarning("No Renderer found on " + gameObject.name);
            enabled = false;
            return;
        }

        objectRenderer.material = materials[currentIndex]; // Start with the first material
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0f;
            currentIndex = 1 - currentIndex; // Toggle between 0 and 1
            objectRenderer.material = materials[currentIndex];
        }
    }
}
