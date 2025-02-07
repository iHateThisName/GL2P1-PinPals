using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private EnumPowerUp power = EnumPowerUp.Shrink;

    void Start()
    {
        // power = (EnumPowerUp)Random.Range(1, 6);
    }

    public void OnTriggerEnter(Collider player)
    {
        PlayerPowerController playerPowerController = player.gameObject.GetComponent<ModelController>().PlayerPowerController;
        playerPowerController.GivePlayerPower(power);
        Debug.Log(player.gameObject.name);
    }
}
