using System.Collections;
using UnityEngine;

public class ExplosionPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject bombModel;
    [SerializeField] private GameObject explosionEffect;
    private GameObject _bombOwner;
    private bool _isDangerous = false;
    [SerializeField] private AudioClip _bombTickSFX;
    [SerializeField] private AudioClip _bombExplodeSFX;

    [SerializeField]
    private PlayerJoinManager respawnManager;

    public void Start()
    {
        //PlayerPowerController bombSFX = GetComponent<PlayerPowerController>().bombSFX();
        SoundEffectManager.Instance.PlaySoundFXClip(this._bombTickSFX, this.gameObject.transform, 1f);
        StartCoroutine(DestroyExplosion());
    }

    public void FixedUpdate()
    {
        Debug.Log("_bombOwner = " + this._bombOwner.name);
    }

    public void AssignBombOwner(GameObject bo)
    {
        this._bombOwner = bo;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!this._isDangerous) return;

        // Detect every player in the collider then respawn them
        if (other.gameObject != _bombOwner)
        {

            if (other.gameObject.tag.StartsWith("Player"))
            {
                EnumPlayerTag tag = other.gameObject.GetComponent<ModelController>().GetPlayerTag();
                respawnManager.Respawn(tag);
            }
            if (other.gameObject.tag == ("Bumper"))
            {
                Destroy(other.gameObject);
            }
        }

    }

    private IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(3f); // Audio cue
        this._isDangerous = true;
        this.explosionEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        this.bombModel.SetActive(false);
        Debug.Log("All Players except you are dead");
        SoundEffectManager.Instance.PlaySoundFXClip(this._bombExplodeSFX, this.gameObject.transform, 1f);
        yield return new WaitForSeconds(2f); // Bomb animation
        Destroy(gameObject);
    }
}
