using System.Collections;
using UnityEngine;
// Hilmir
public class MinefieldPowerUp : MonoBehaviour {
    [SerializeField] private GameObject _mineModel;
    [SerializeField] private GameObject _mineAddon;
    [SerializeField] private GameObject _mineDetector;
    [SerializeField] private GameObject _mineExplosionEffect;
    private PlayerReferences _mineOwner;
    private bool _isDangerous = false;
    private int _point = 1;
    private int _points = 500;
    [SerializeField] private AudioClip _mineIdleSFX;
    [SerializeField] private AudioClip _mineExplosionSFX;
    void Start() {
        SoundEffectManager.Instance.PlaySoundFXClip(this._mineIdleSFX, this.gameObject.transform, 1f);
        GetComponent<Rigidbody>();
        StartCoroutine(PrimeMine());
    }

    public void AssignMineOwner(GameObject mo) {
        this._mineOwner = mo.GetComponent<PlayerReferences>();
    }
    public void OnTriggerStay(Collider other) {
        if (other.gameObject.tag.StartsWith("Player")) {
            if (!this._isDangerous) return;

            PlayerReferences playerRef = other.gameObject.GetComponent<PlayerReferences>();
            StartCoroutine(MineExplosion());
            //VFXManager.Instance.SpawnVFX(VFXType.PlayerExplosion, other.transform.position, duration: 2f);
            VFXManager.Instance.SpawnVFX(VFXType.ThanosSnapGray, other.transform.position, duration: 10f);
            EnumPlayerTag tag = other.gameObject.GetComponent<PlayerReferences>().GetPlayerTag();
            PlayerJoinManager.Instance.Respawn(tag);
            Debug.Log(tag.ToString());
            if (tag != _mineOwner.GetPlayerTag())
            {
                _mineOwner.PlayerStats.PlayerKills(1);
            }
            playerRef.PlayerStats.PlayerDeaths(_point);
            playerRef.PlayerScoreTracker.DockPoints(_points);
            //Debug.Log("player has respawned");
        } else if (other.gameObject.tag.StartsWith("Ground")) {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //print("mine is frozen");
        } else {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    private IEnumerator MineExplosion() {
        this._mineExplosionEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f); // Time until the Mine Game Object gets despawned
        this._mineModel.SetActive(false);
        this._mineAddon.SetActive(false);
        this._mineDetector.SetActive(false);
        GetComponent<Collider>().enabled = false;
        SoundEffectManager.Instance.PlaySoundFXClip(this._mineExplosionSFX, this.gameObject.transform, 1f);
        yield return new WaitForSeconds(2f); // Bomb animation
        Destroy(gameObject);
    }

    private IEnumerator PrimeMine() {
        yield return new WaitForSeconds(2f);
        this._isDangerous = true;
    }

}
