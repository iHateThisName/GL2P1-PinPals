using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private PlayerPowerController powerController;
    [SerializeField] private PlayerReferences ModelController;

    private void Start()
    {
        powerController = ModelController.PlayerPowerController;
    }

    private void OnCollisionEnter(Collision collision)
    {
        powerController.PlayerCollision(collision);
        collision.gameObject.GetComponent<PlayerReferences>().PlayerStats.PlayerKills(1);
    }
}
