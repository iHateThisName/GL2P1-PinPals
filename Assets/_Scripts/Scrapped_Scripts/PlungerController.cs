using System.Collections;
using UnityEngine;

public class PlungerController : MonoBehaviour
{
    public Vector3 retractPosition = new Vector3(0, 0, -2f); // Position where the launchpad will retract
    public Vector3 originalPosition = new Vector3(0, 0, 0); // The original position of the launchpad
    public float springConstant = 500f; // Spring constant to give enough force for launching (adjust as needed)
    public float dampingCoefficient = 10f; // Damping coefficient to control oscillations
    public KeyCode retractButton = KeyCode.Space; // Button to retract the launchpad

    private Vector3 velocity = Vector3.zero;  // Initial velocity for the damping system
    private bool isRetracted = false; // To track if the plunger has fully retracted
    private bool isButtonPressed = false; // To track if the retract button is held down
    private bool isReturning = false; // Flag to check if the plunger is returning to the original position
    private Collider plungerCollider; // The collider attached to the plunger to detect nearby objects

    [SerializeField] private EnumPlayerTag AssignedPlayer; // The player assigned to this plunger
    private void Start()
    {
        // Get the collider of the plunger
        plungerCollider = GetComponent<Collider>();
        if (plungerCollider == null)
        {
            Debug.LogWarning("Plunger does not have a collider. Please attach a collider to it.");
        }
    }

    private void Update()
    {
        // Retract the launchpad when the retractButton is pressed
        if (Input.GetKey(retractButton) && !isButtonPressed && !isReturning)
        {
            StartCoroutine(MoveLaunchpad(retractPosition, springConstant));
            isButtonPressed = true;
        }

        // After reaching the retracted position, launch the object(s)
        if (isRetracted && !Input.GetKey(retractButton) && isButtonPressed && !isReturning)
        {
            LaunchObjectsInRange();
            isButtonPressed = false;
            StartCoroutine(MoveLaunchpad(originalPosition, springConstant)); // Start moving back to the original position
            isReturning = true; // Mark that the plunger is returning
        }

        // Apply spring force and damping force
        if (Vector3.Distance(transform.position, retractPosition) > 0.1f && Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            ApplySpringDamperForce();
        }

        if (GameManager.Instance.IsPlayerHoldingInteraction(this.AssignedPlayer)) {
            // The player is holding the interaction button
        } else {
            // The player stopped holding the interaction button
        }
    }

    // Coroutine to move the launchpad smoothly between two positions (spring-based)
    private IEnumerator MoveLaunchpad(Vector3 targetPosition, float springStrength)
    {
        // Move smoothly towards the target position
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, springStrength * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        if (targetPosition == retractPosition)
        {
            isRetracted = true; // Mark the plunger as fully retracted
        }
        else
        {
            isRetracted = false;
            isReturning = false; // Plunger has returned to the original position
        }
    }

    // Apply spring force and damping force to the plunger
    private void ApplySpringDamperForce()
    {
        Vector3 displacement = transform.position - originalPosition;
        Vector3 springForce = -springConstant * displacement;
        Vector3 dampingForce = -dampingCoefficient * velocity;

        Vector3 totalForce = springForce + dampingForce;

        velocity += totalForce * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        // Stop the plunger if it reaches the original or retract position
        if (Vector3.Distance(transform.position, retractPosition) < 0.1f)
        {
            transform.position = retractPosition;
            velocity = Vector3.zero;
        }
        if (Vector3.Distance(transform.position, originalPosition) < 0.1f)
        {
            transform.position = originalPosition;
            velocity = Vector3.zero;
        }
    }

    // Launch all objects within the plunger's trigger area
    private void LaunchObjectsInRange()
    {
        // Find all colliders within the trigger area
        Collider[] colliders = Physics.OverlapBox(plungerCollider.bounds.center, plungerCollider.bounds.extents);

        foreach (var collider in colliders)
        {
            // Check if the collider has a Rigidbody attached and is within the trigger
            if (collider.CompareTag("Player01") && collider.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();

                // Apply force to the object to launch it in the direction of the Z-axis
                rb.AddForce(transform.forward * springConstant * 0.1f, ForceMode.Impulse);
            }
        }
    }

    // This method is called when something enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has a Rigidbody and is tagged as Launchable
        if (other.CompareTag("Player01") && other.GetComponent<Rigidbody>() != null)
        {
            // Apply force to the object when it enters the trigger area
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(transform.up * springConstant * 0.1f, ForceMode.Impulse);
        }
    }
}