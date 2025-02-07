using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    private EnumPowerUp power = EnumPowerUp.None;

    void Start()
    {
        power = (EnumPowerUp)Random.Range(1, 3);
    }

    public IEnumerator OnTriggerEnter(Collider player)
    {
        PlayerPowerController playerPowerController = player.gameObject.GetComponent<ModelController>().PlayerPowerController;
        playerPowerController.GivePlayerPower(power);
        Debug.Log(player.gameObject.name);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        power = (EnumPowerUp)Random.Range(1, 3);
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
