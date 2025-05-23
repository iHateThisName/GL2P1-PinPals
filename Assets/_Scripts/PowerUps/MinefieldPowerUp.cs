using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Hilmir
public class MinefieldPowerUp : MonoBehaviour {
    [SerializeField] private GameObject _mineModel;
    [SerializeField] private GameObject _mineExplosionEffect;
    private PlayerReferences _mineOwner;
    private bool _isDangerous = false;
    private int _point = 1;
    private int _points = 500;
    [SerializeField] private AudioClip _mineIdleSFX;
    [SerializeField] private AudioClip _mineExplosionSFX;

    private List<EnumPlayerTag> _killedPlayers = new List<EnumPlayerTag>();
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
            EnumPlayerTag tag = playerRef.GetPlayerTag();

            if (this._killedPlayers.Contains(tag))
                return;
            this._killedPlayers.Add(tag);

            StartCoroutine(MineExplosion(tag));
            VFXManager.Instance.SpawnVFX(VFXType.PlayerExplosion, other.transform.position, duration: 2f);
            //VFXManager.Instance.SpawnVFX(VFXType.ThanosSnapGray, other.transform.position, duration: 10f);
            //PlayerJoinManager.Instance.Respawn(tag);
            //OnPlayerDeath();
            
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
    private IEnumerator MineExplosion(EnumPlayerTag tag) {
        this._mineExplosionEffect.SetActive(true);
        GameManager.Instance.GetPlayerReferences(tag).rb.isKinematic = true;
        yield return new WaitForSeconds(0.2f); // Time until the Mine Game Object gets despawned
        this._mineModel.SetActive(false);
        GetComponent<Collider>().enabled = false;
        SoundEffectManager.Instance.PlaySoundFXClip(this._mineExplosionSFX, this.gameObject.transform, 1f);
        yield return StartCoroutine(PlayerJoinManager.Instance.RespawnDelay(tag, EnumPlayerAnimation.AshDeath));
        Destroy(gameObject);
    }

    private IEnumerator PrimeMine() {
        yield return new WaitForSeconds(2f);
        this._isDangerous = true;
    }

}
