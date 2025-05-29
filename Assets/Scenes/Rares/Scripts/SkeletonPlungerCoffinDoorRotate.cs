using UnityEngine;

public class SkeletonPlungerCoffinDoorRotate : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAmount = new Vector3(0, 0, 45); // Rotation applied while holding space
    public float rotationSpeed = 5f; // Speed of rotation

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool isHolding = false;

    void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = initialRotation * Quaternion.Euler(rotationAmount);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isHolding = true;
        }
        else
        {
            isHolding = false;
        }

        // Smoothly rotate toward target or back to initial
        if (isHolding)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, Time.deltaTime * rotationSpeed);
        }
    }
}

