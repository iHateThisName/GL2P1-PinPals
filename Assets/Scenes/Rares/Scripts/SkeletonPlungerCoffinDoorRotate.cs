using UnityEngine;

public class SkeletonPlungerCoffinDoorRotate : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAmount = new Vector3(0, 0, 45); // Rotation applied while holding space
    public float rotationSpeed = 5f; // Speed of rotation

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool isHolding = false;

    [SerializeField] private EnumPlayerTag playerTag = EnumPlayerTag.Player01;

    void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = initialRotation * Quaternion.Euler(rotationAmount);
    }

    void Update()
    {
        isHolding = GameManager.Instance.IsPlayerHoldingInteraction(this.playerTag);
        if (isHolding)
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

