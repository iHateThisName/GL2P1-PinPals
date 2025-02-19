using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    private EnumPowerUp power; //= EnumPowerUp.Balloon;

    void Start()
    {
        power = (EnumPowerUp)Random.Range(1, 4);
    }

    public IEnumerator OnTriggerEnter(Collider player)
    {
        PlayerPowerController playerPowerController = player.gameObject.GetComponent<ModelController>().PlayerPowerController;
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();

        playerPowerController.GivePlayerPower(power);
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(5f);
        power = (EnumPowerUp)Random.Range(1, 4);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;

    }

}
