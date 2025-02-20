using UnityEngine;

public class ExplosionPowerUp : MonoBehaviour
{
    [SerializeField]
    public Transform spawnPoint;
    public void OnCollisionEnter(Collision collision)
    {
            collision.transform.position = spawnPoint.position;
    }
}
