using System.Collections;
using UnityEngine;
// Hilmir & Ivar
public class ExplosionPowerUp : MonoBehaviour {
    [SerializeField] private GameObject _bombModel;
    [SerializeField] private GameObject _explosionEffect;
    private GameObject _bombOwner;
    private int _point = 1;
    private bool _isDangerous = false;
    [SerializeField] private AudioClip _bombTickSFX;
    [SerializeField] private AudioClip _bombExplodeSFX;

    public void Start() {
        StartCoroutine(DestroyExplosion());
    }


    public void AssignBombOwner(GameObject bo) {
        this._bombOwner = bo;
    }

    private void OnTriggerStay(Collider player) {

        if (!this._isDangerous) return;

        // Detect every player in the collider then respawn them
        if (player.gameObject.name.Contains("Clone")) {
            Destroy(player.gameObject);
            _bombOwner.gameObject.GetComponent<PlayerReferences>().PlayerStats.PlayerKills(_point);
            return;
        } else if (player.gameObject.tag.StartsWith("Player")) {
            // To spawn an explosion VFX
            //VFXManager.Instance.SpawnVFX(VFXType.PlayerExplosion, player.transform.position);
            EnumPlayerTag tag = player.gameObject.GetComponent<PlayerReferences>().GetPlayerTag();
            PlayerJoinManager.Instance.Respawn(tag);
            _bombOwner.gameObject.GetComponent<PlayerReferences>().PlayerStats.PlayerKills(_point);
            player.gameObject.GetComponent<PlayerReferences>().PlayerStats.PlayerDeaths(_point);
        }
        if (player.gameObject.tag == ("Bumper")) {
            Destroy(player.gameObject);
        }

    }

    private IEnumerator DestroyExplosion() {
        SoundEffectManager.Instance.PlaySoundFXClip(this._bombTickSFX, this.gameObject.transform, 1f);
        yield return new WaitForSeconds(3f); // Audio cue
        this._isDangerous = true;
        this._explosionEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        this._bombModel.SetActive(false);
        Debug.Log("All Players except you are dead");
        SoundEffectManager.Instance.PlaySoundFXClip(this._bombExplodeSFX, this.gameObject.transform, 1f);
        yield return new WaitForSeconds(2f); // Bomb animation
        Destroy(this.gameObject);
    }
}
