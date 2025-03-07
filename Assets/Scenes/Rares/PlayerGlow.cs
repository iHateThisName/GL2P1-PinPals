using UnityEngine;
using System.Collections;

public class PlayerGloww : MonoBehaviour
{
    public Material originalMaterial; // Assign the original material in the Inspector
    public Material glowMaterial; // Assign the glowing material in the Inspector
    private Renderer ballRenderer;
    private bool isGlowing = false;
    private float transitionDuration = 1f; // Time for the transition

    void Start()
    {
        ballRenderer = GetComponent<Renderer>();
        ballRenderer.material = originalMaterial; // Start with the original material
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isGlowing = !isGlowing;
            StartCoroutine(TransitionMaterial(isGlowing ? glowMaterial : originalMaterial));
        }
    }

    IEnumerator TransitionMaterial(Material targetMaterial)
    {
        Material startMaterial = ballRenderer.material;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            // Smoothstep for a nicer easing effect
            ballRenderer.material.Lerp(startMaterial, targetMaterial, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        ballRenderer.material = targetMaterial; // Ensure final material is properly set
    }
}
