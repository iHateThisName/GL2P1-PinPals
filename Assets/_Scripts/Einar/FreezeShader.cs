using UnityEngine;
using System.Collections;
using UnityEngine.VFX;

public class FreezeShader : MonoBehaviour
{
    [SerializeField] private MeshRenderer sphereSkin;
    [SerializeField] private Material material1; // Transition start
    [SerializeField] private Material material2; // Transition end
    //[SerializeField] private VisualEffect frostSmoke;

    private Material currentMaterial;

    private Coroutine transitionCoroutine;

    private void Start()
    {
        //// Optional: create an instance to avoid affecting shared material
        //sphereSkin.material = Instantiate(sphereSkin.material);
        //currentMaterial = sphereSkin.material;
        {
            // Instantly switch to material1
            sphereSkin.material = Instantiate(material1);
            currentMaterial = sphereSkin.material;

            // Play particle effect
            //if (frostSmoke != null)
            // frostSmoke.Play();

            // If a transition is already running, stop it
            if (transitionCoroutine != null)
                StopCoroutine(transitionCoroutine);

            // Start the transition from material1 to material2
            transitionCoroutine = StartCoroutine(TransitionMaterials());
        }
    }

    private void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    // Instantly switch to material1
        //    sphereSkin.material = Instantiate(material1);
        //    currentMaterial = sphereSkin.material;

        //    // Play particle effect
        //    //if (frostSmoke != null)
        //       // frostSmoke.Play();

        //    // If a transition is already running, stop it
        //    if (transitionCoroutine != null)
        //        StopCoroutine(transitionCoroutine);

        //    // Start the transition from material1 to material2
        //    transitionCoroutine = StartCoroutine(TransitionMaterials());
        //}
    }

    private IEnumerator TransitionMaterials()
    {
        float duration = 1.5f; // transition duration in seconds
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // Here, you need a way to blend materials
            // For example, if your shader supports a "_Blend" float:
            // currentMaterial.SetFloat("_Blend", t);

            // Or, if you just want to swap textures or colors:
            // You can interpolate a property, or swap materials at the end.

            // Example: If blending is supported:
            if (currentMaterial.HasProperty("_Blend"))
            {
                currentMaterial.SetFloat("_Blend", t);
            }
            else
            {
                // If your shader does not support blending, you might need to
                // manually interpolate properties or swap at the end
            }

            yield return null;
        }

        // Ensure final state
        // If your shader supports blending, set to 1
        if (currentMaterial.HasProperty("_Blend"))
        {
            currentMaterial.SetFloat("_Blend", 1f);
        }
        else
        {
            // Alternatively, swap to material2
            sphereSkin.material = Instantiate(material2);
            currentMaterial = sphereSkin.material;
        }
    }
}
