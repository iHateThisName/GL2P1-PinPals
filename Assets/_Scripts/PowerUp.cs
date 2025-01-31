using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private EnumPowerUp power = EnumPowerUp.Shrink;

    /*void Start()
    {
        power = (EnumPowerUp)Random.Range(1, 6);
    }*/

    public void OnTriggerEnter(Collider player)
    {
        player.gameObject.GetComponentInParent<PlayerPowerController>().GivePlayerPower(power);
        Debug.Log(player.gameObject.name);
    }
}
