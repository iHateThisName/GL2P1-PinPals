using UnityEngine;

public class Chutes : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;


    private void OnTriggerEnter(Collider player)
    {
        player.transform.position = spawnPoint.position;
        player.GetComponent<Rigidbody>().Sleep();
    }
}
