using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    private EnumPowerUp power;
    [SerializeField] private AudioClip powerUpPickUp;

    void Start() {
        power = (EnumPowerUp)Random.Range(1, 6);
    }

    public IEnumerator OnTriggerEnter(Collider player) {
        PlayerPowerController playerPowerController = player.gameObject.GetComponent<ModelController>().PlayerPowerController;
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
        SoundEffectManager.Instance.PlaySoundFXClip(powerUpPickUp, transform, 1f);

        playerPowerController.GivePlayerPower(power);
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(5f);
        power = (EnumPowerUp)Random.Range(1, 6);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;

    }

}
