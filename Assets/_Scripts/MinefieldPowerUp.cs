using System.Collections;
using UnityEngine;

public class MinefieldPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject _mineModel;
    [SerializeField] private GameObject _mineAddon;
    [SerializeField] private GameObject _mineDetector;
    [SerializeField] private GameObject _mineExplosionEffect;
    private bool _isDangerous = false;
    private int _point = 1;
    [SerializeField] private AudioClip _mineIdleSFX;
    [SerializeField] private AudioClip _mineExplosionSFX;
    void Start()
    {
        SoundEffectManager.Instance.PlaySoundFXClip(this._mineIdleSFX, this.gameObject.transform, 1f);
        GetComponent<Rigidbody>();
        StartCoroutine(PrimeMine());
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.StartsWith("Player"))
        {
            if (!this._isDangerous) return;

            StartCoroutine(MineExplosion());
            EnumPlayerTag tag = other.gameObject.GetComponent<ModelController>().GetPlayerTag();
            PlayerJoinManager.Instance.Respawn(tag);
            other.gameObject.GetComponent<ModelController>().PlayerStats.PlayerKills(_point);
            Debug.Log("player has respawned");
        }
        else if (other.gameObject.tag.StartsWith("Ground"))
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            print("mine is frozen");
        } else
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private IEnumerator MineExplosion()
    {
        this._mineExplosionEffect.SetActive(true); 
        yield return new WaitForSeconds(0.2f); // Time until the Mine Game Object gets despawned
        this._mineModel.SetActive(false);
        this._mineAddon.SetActive(false);
        this._mineDetector.SetActive(false);
        SoundEffectManager.Instance.PlaySoundFXClip(this._mineExplosionSFX, this.gameObject.transform, 1f);
        yield return new WaitForSeconds(2f); // Bomb animation
        Destroy(gameObject);
    }

    private IEnumerator PrimeMine()
    {
        yield return new WaitForSeconds(2f);
        this._isDangerous = true;
    }


}
