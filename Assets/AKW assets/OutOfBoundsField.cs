using UnityEngine;

public class OutOfBoundsField : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = spawnPoint.position;
        other.GetComponent<Rigidbody>().Sleep();
    }
}
