using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedSpawnpoint;
    private Transform transformSpawnpoint;

    public GameObject playerPrefab;
    private GameObject newPlayer;

    public void Start()
    {
        transformSpawnpoint = selectedSpawnpoint.transform;
    }
    public void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("DeathBarrier"))
        {
            Invoke("Respawn", 3f);
        }
    }

    public void Respawn()
    {
        Debug.Log("You are now dead");
        Destroy(gameObject);
        newPlayer = Instantiate(playerPrefab, transformSpawnpoint.position, transformSpawnpoint.rotation);
    }
}
