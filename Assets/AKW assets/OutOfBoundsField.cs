using UnityEngine;

public class OutOfBoundsField : MonoBehaviour
{
    public Vector3 teleportLocation;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = teleportLocation;
    }
}
