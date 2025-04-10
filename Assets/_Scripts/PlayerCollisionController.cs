using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private PlayerPowerController powerController;
    private EnumPowerUp enumPowerUp;
    [SerializeField] private ModelController ModelController;
    private EnumPlayerTag _playerTag;
    private int _point = 1;

    private void Start()
    {
        this._playerTag = this.ModelController.GetPlayerTag();
        powerController = ModelController.PlayerPowerController;
    }

    private void OnCollisionEnter(Collision collision)
    {
        powerController.PlayerCollision(collision);
        collision.gameObject.GetComponent<ModelController>().PlayerStats.PlayerKills(_point);
    }
}
