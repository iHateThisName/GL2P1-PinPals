using UnityEngine;
public class SkeletonGlow : MonoBehaviour
{
    public Material normalMaterial;  //Original or new
    public Material glowMaterial;    //The glow material
    public float cycleDuration = 20f; //Full cycle
    public float glowTriggerTime = 15f;// When it will trigger the glow
    public float glowDuration = 0f;//Glow should go away in * seconds. Leave 0 to not make effect.

    private Renderer objectRenderer;
    private Material originalMaterial;
    private float timer = 0f;
    private float glowStartTime = 0f;
    private bool hasSwitchedToGlow = false;
    private bool glowEnded = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            enabled = false;
            return;
        }

        //Save the original material if not assigned
        originalMaterial = normalMaterial != null ? normalMaterial : objectRenderer.material;
        objectRenderer.material = originalMaterial;
    }

    void Update()
    {
        timer += Time.deltaTime;

        //Trigger the glow
        if (!hasSwitchedToGlow && timer >= glowTriggerTime)
        {
            if (glowMaterial != null)
            {
                objectRenderer.material = glowMaterial;
                glowStartTime = timer;
                hasSwitchedToGlow = true;
                glowEnded = false;
            }
        }

        //If glowDuration > 0, disable it
        if (hasSwitchedToGlow && glowDuration > 0f && !glowEnded)
        {
            if (timer >= glowStartTime + glowDuration)
            {
                objectRenderer.material = originalMaterial;
                glowEnded = true;
            }
        }

        //End of cyckle
        if (timer >= cycleDuration)
        {
            objectRenderer.material = originalMaterial;
            timer = 0f;
            hasSwitchedToGlow = false;
            glowEnded = false;
        }
    }
}
