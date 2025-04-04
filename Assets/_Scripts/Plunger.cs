using UnityEngine;

public class Plunger : MonoBehaviour
{
    private Rigidbody targetBall;
    public float maxPull = 10f; // How far it can move backward
    public float launchForce = 500f; // Force applied to ball
    private Vector3 startPos;
    private float pullAmount = 0f;
    private bool isPulling = false;
    //[SerializeField] private Collider _triggerZone;
    [SerializeField] private EnumPlayerTag AssignedPlayer; // The player assigned to this plunger
    private bool allowUsePlunger = false;

    [SerializeField] private AudioClip plungerSFX;

    void Start()
    {
        startPos = transform.position - new Vector3(0, 0, 0); // Keeping this for flexibility
    }

    void Update()
    {
        if (!allowUsePlunger) return;
        //if (Input.GetKey(KeyCode.S))
        //{
        //    isPulling = true;
        //    pullAmount = Mathf.Min(pullAmount + Time.deltaTime * 3, maxPull);
        //    transform.position = Vector3.Lerp(startPos, startPos + new Vector3(0, 0, -maxPull), pullAmount / maxPull);
        //}

        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    LaunchBall();
        //    isPulling = false;
        //    transform.position = startPos;
        //    pullAmount = 0;
        //}

        if (GameManager.Instance.IsPlayerHoldingInteraction(this.AssignedPlayer))
        {
            // The player is holding the interaction button
            isPulling = true;
            pullAmount = Mathf.Min(pullAmount + Time.deltaTime * 12, maxPull);
            transform.position = Vector3.Lerp(startPos, startPos + new Vector3(0, 0, -maxPull), pullAmount / maxPull);
        }
        else
        {
            // The player stopped holding the interaction button
            LaunchBall();
            isPulling = false;
            transform.position = startPos;
            pullAmount = 0;
        }
    }

    void LaunchBall()
    {
        if (targetBall != null)
        {
            targetBall.AddForce(Vector3.forward * launchForce * (pullAmount / maxPull), ForceMode.Impulse);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with has a Rigidbody and set it as the target
        if (other.attachedRigidbody != null)
        {
            allowUsePlunger = true;
            targetBall = other.attachedRigidbody;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reset the target when the object exits the trigger
        if (other.attachedRigidbody == targetBall)
        {
            allowUsePlunger = false;
            targetBall = null;
            SoundEffectManager.Instance.PlaySoundFXClip(plungerSFX, transform, 1f);
        }
    }
}
