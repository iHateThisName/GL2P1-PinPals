using UnityEngine;

public class WideWestRevolverPlunger : MonoBehaviour {
    [Header("Rotation Settings")]
    public Vector3 rotationAmount = new Vector3(0, 0, 45); // Rotation applied while holding space
    public float rotationSpeed = 5f; // Speed of rotation

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip pressClip;
    public AudioClip releaseClip;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool isHolding = false;
    private bool wasHolding = false;

    [SerializeField] private EnumPlayerTag playerTag = EnumPlayerTag.Player01;

    void Start() {
        initialRotation = transform.rotation;
        targetRotation = initialRotation * Quaternion.Euler(rotationAmount);
    }

    void Update() {
        isHolding = GameManager.Instance.IsPlayerHoldingInteraction(this.playerTag);
        if (isHolding && !wasHolding) {
            if (pressClip != null && audioSource != null) {
                audioSource.PlayOneShot(pressClip);
            }
        }
        if (!isHolding && wasHolding) {
            if (releaseClip != null && audioSource != null) {
                audioSource.PlayOneShot(releaseClip);
            }
        }
        if (isHolding) {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        } else {
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, Time.deltaTime * rotationSpeed);
        }
        wasHolding = isHolding;
    }
}
