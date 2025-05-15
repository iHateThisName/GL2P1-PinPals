using UnityEngine;

public class SkeletonGlow : MonoBehaviour
{
    [Tooltip("Optional. If not set, the script will use the object's original material.")]
    public Material normalMaterial;  // Optional: assign or leave empty to use original
    public Material glowMaterial;    // The glowing material

    [Tooltip("Duration of the full cycle in seconds.")]
    public float cycleDuration = 20f;

    [Tooltip("At what second in the cycle the glow should activate.")]
    public float glowTriggerTime = 15f;

    private Renderer objectRenderer;
    private Material originalMaterial;
    private float timer = 0f;
    private bool hasSwitchedToGlow = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("SkeletonGlow: No Renderer found on this GameObject.");
            enabled = false;
            return;
        }

        // Save the original material if not assigned manually
        originalMaterial = normalMaterial != null ? normalMaterial : objectRenderer.material;
        objectRenderer.material = originalMaterial;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Switch to glow material at the trigger time
        if (!hasSwitchedToGlow && timer >= glowTriggerTime)
        {
            if (glowMaterial != null)
            {
                objectRenderer.material = glowMaterial;
                hasSwitchedToGlow = true;
            }
        }

        // Reset the cycle
        if (timer >= cycleDuration)
        {
            objectRenderer.material = originalMaterial;
            timer = 0f;
            hasSwitchedToGlow = false;
        }
    }
}
