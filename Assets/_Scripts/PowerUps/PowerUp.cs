using System.Collections;
using UnityEngine;
// Hilmir & Ivar
public class PowerUp : MonoBehaviour {
    private EnumPowerUp power;
    private int _point = 1;
    [SerializeField] private AudioClip powerUpPickUp;
    [SerializeField] private bool isSpinie = true;

    void Start() {
        power = (EnumPowerUp)Random.Range(4, 6);
    }

    void Update() {
        if (isSpinie) {
            transform.GetChild(0).localRotation = Quaternion.Euler(45f, Time.time * 100f, 45f);
        }
    }

    public IEnumerator OnTriggerEnter(Collider player) {
        if (player.gameObject.name.Contains("Clone"))
            yield break;

        PlayerPowerController playerPowerController = player.gameObject.GetComponent<PlayerReferences>().PlayerPowerController;
        MeshRenderer meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
        SoundEffectManager.Instance.PlaySoundFXClip(powerUpPickUp, transform, 1f);
        PlayerReferences modelController = player.gameObject.GetComponent<PlayerReferences>();

        playerPowerController.GivePlayerPower(power);
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(5f);
        power = (EnumPowerUp)Random.Range(4, 6);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
    }
}
