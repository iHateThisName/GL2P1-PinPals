using System.Collections;
using UnityEngine;

public class PlungerController : MonoBehaviour
{
    public Vector3 retractPosition = new Vector3(0, 0, -2f); // The position to which the launchpad will retract (along -Z axis)
    public Vector3 originalPosition = new Vector3(0, 0, 0); // The original position of the launchpad
    public float retractSpeed = 2f;  // Speed at which the launchpad retracts
    public float returnSpeed = 10f;  // Speed at which the launchpad returns to the original position
    public float springConstant = 10f; // Spring constant (how stiff the spring is)
    public KeyCode retractButton = KeyCode.Space; // Button to retract the launchpad
    private bool isRetracted = false; // To check if the launchpad is retracted
    private bool isButtonPressed = false; // To track if the retract button is held down

    private void Update()
    {
        // Retract the launchpad when the retractButton is pressed
        if (Input.GetKey(retractButton) && !isButtonPressed)
        {
            // Start retracting the launchpad smoothly
            StartCoroutine(MoveLaunchpad(retractPosition, retractSpeed));
            isButtonPressed = true; // Mark that the button has been pressed
        }

        // When the launchpad has fully retracted, allow it to return when the button is released
        if (transform.position == retractPosition)
        {
            isRetracted = true;
        }

        // Move the launchpad back to its original position when the retractButton is released
        if (isRetracted && !Input.GetKey(retractButton) && isButtonPressed)
        {
            StartCoroutine(MoveLaunchpad(originalPosition, returnSpeed)); // Move back quickly
            isButtonPressed = false; // Reset the button pressed state
        }

        // Apply spring force only if the launchpad is not at the retract position
        if (transform.position != retractPosition && transform.position != originalPosition)
        {
            ApplySpringForce();
        }
    }

    // Coroutine to move the launchpad smoothly between two positions
    private IEnumerator MoveLaunchpad(Vector3 targetPosition, float speed)
    {
        // Move smoothly towards the target position until it is reached
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        // Ensure the position is exactly the target when done (this ensures we stop at the target)
        transform.position = targetPosition;
    }

    // Apply the spring force to the launchpad
    private void ApplySpringForce()
    {
        // Calculate the compression (how far from the original position the launchpad is)
        float compression = Vector3.Distance(transform.position, originalPosition);

        // Apply spring force only if the launchpad is not at the retracted position
        if (transform.position != retractPosition)
        {
            // Calculate the spring force using Hooke's law (F = -k * x)
            float springForce = -springConstant * compression;

            // Apply the spring force to move the launchpad back towards the original position
            if (transform.position != originalPosition)
            {
                transform.position += (originalPosition - transform.position).normalized * springForce * Time.deltaTime;
            }
        }

        // Forcefully clamp the position to the retract position if it's within a small threshold
        if (Vector3.Distance(transform.position, retractPosition) < 0.1f)
        {
            transform.position = retractPosition;
        }
    }
}
