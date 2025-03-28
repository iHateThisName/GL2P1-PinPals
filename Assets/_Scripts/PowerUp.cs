using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    private EnumPowerUp power;
    [SerializeField] private AudioClip powerUpPickUp;
    [SerializeField] private bool isSpinie = true;

    void Start() {
        power = (EnumPowerUp)Random.Range(1, 5);
    }

    void Update()
    {
        if (isSpinie)
        {
            transform.localRotation = Quaternion.Euler(45f, Time.time * 100f, 45f);
        }
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
        power = (EnumPowerUp)Random.Range(1, 5);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;

    }

}
