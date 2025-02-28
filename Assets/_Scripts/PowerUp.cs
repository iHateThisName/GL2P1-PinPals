using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class PowerUp : MonoBehaviour
{
    private EnumPowerUp power; //= EnumPowerUp.Balloon;
    [SerializeField] private AudioClip powerUpPickUp;

    void Start()
    {
        power = (EnumPowerUp)Random.Range(1, 5);
    }

    public IEnumerator OnTriggerEnter(Collider player)
    {
        PlayerPowerController playerPowerController = player.gameObject.GetComponent<ModelController>().PlayerPowerController;
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
        SoundEffectManager.Instance.PlaySoundFXClip(powerUpPickUp, transform, 1f);

        playerPowerController.GivePlayerPower(power);
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(5f);
        power = (EnumPowerUp)Random.Range(1, 5);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;

    }

}
