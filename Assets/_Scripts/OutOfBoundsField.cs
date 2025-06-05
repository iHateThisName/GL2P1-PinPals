using UnityEngine;
//Andreas, Einar
public class OutOfBoundsField : MonoBehaviour {
    private int _point = 1;
    [SerializeField] private AudioClip fallDeathSFX;

    private void OnTriggerEnter(Collider player) {
        if (player.gameObject.name.Contains("Clone")) {
            Destroy(player.gameObject);
            return;
        }

        PlayerReferences controller = player.gameObject.GetComponent<PlayerReferences>();
        EnumPlayerTag tag = controller.GetPlayerTag();
        PlayerPowerController power = controller.PlayerPowerController;
        player.gameObject.GetComponent<PlayerReferences>().PlayerStats.PlayerDeaths(_point);
        SoundEffectManager.Instance.PlaySoundFXClip(fallDeathSFX, transform, 1f);
        //Remove powerups before respawning
        power.RemoveCurrentPower();

        PlayerJoinManager.Instance.Respawn(tag);
    }
}
