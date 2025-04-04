using UnityEngine;

public class ForcePushie : MonoBehaviour
{
    public Transform target;
    public float forceStrength = 2f;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            Vector3 forceDirection = (target.position - other.transform.position).normalized;

            rb.AddForce(forceDirection * forceStrength, ForceMode.Impulse);
        }
    }
}
