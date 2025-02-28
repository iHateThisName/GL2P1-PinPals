using UnityEngine;

public class SpringDeformer : MonoBehaviour
{
    public Transform plunger; // Assign your plunger (cylinder) here
    public Transform springStart; // Base of the spring (static)
    public Transform springEnd; // End of the spring (follows plunger)

    private float initialDistance; // Store initial distance
    private Vector3 initialScale; // Store original scale

    void Start()
    {
        initialScale = transform.localScale; // Save the starting scale
        initialDistance = Vector3.Distance(springStart.position, springEnd.position); // Save starting distance
    }

    void Update()
    {
        if (plunger != null)
        {
            // Move springEnd to follow the plunger
            springEnd.position = plunger.position;

            // Calculate new distance
            float currentDistance = Vector3.Distance(springStart.position, springEnd.position);

            // Keep the spring centered between the start and end
            transform.position = (springStart.position + springEnd.position) / 2;

            // Normalize scaling to prevent sudden jumps
            float scaleFactor = currentDistance / initialDistance;
            transform.localScale = new Vector3(initialScale.x, initialScale.y, scaleFactor * initialScale.z);

            // Rotate the spring to match the stretched direction
            transform.rotation = Quaternion.LookRotation(springEnd.position - springStart.position);
        }
    }
}
