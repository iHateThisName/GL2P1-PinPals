using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private PlayerPowerController powerController;
    [SerializeField] private PlayerReferences playerReference;

    private void Start()
    {
        powerController = playerReference.PlayerPowerController;
    }

    private void OnCollisionEnter(Collision collision)
    {
        powerController.PlayerCollision(collision);
        playerReference.PlayerStats.PlayerKills(1);
    }
}
