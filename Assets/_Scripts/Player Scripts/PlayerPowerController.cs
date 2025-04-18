using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
// Hilmir, Ivar and Einar
public class PlayerPowerController : MonoBehaviour {
    [field: SerializeField] public EnumPowerUp currentPower { get; private set; } = EnumPowerUp.None;
    [SerializeField] private PlayerReferences playerReferences;
    [SerializeField] private Transform powerupPlayerTransform;
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private GameObject _playerModel;
    private Vector3 defaultScale;
    private float _powerUpCooldown = 5f;
    private float _originalMass = 0.02f;
    private int _point = 1;
    private bool _isPlayerDead;
    [SerializeField] private float shrinkMass = 0.01f;
    [SerializeField] private float growMass = 0.05f;
    [SerializeField] private float shrinkScale = 2f;
    [SerializeField] private float growScale = 10f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private GameObject balloonPrefab;
    [SerializeField] private GameObject honeyPrefab;

    [Header("Power up SFX")]
    [SerializeField] private AudioClip multiBallSFX;
    [SerializeField] private AudioClip freezeSFX;
    [SerializeField] private AudioClip growSFX;
    [SerializeField] private AudioClip shrinkSFX;
    [SerializeField] private AudioClip bombTickSFX;

    [SerializeField] private PlayerFollowCanvasManager playerText;

    private EnumPlayerTag _assignedPlayerTag;
    private bool _isPowerActivated = false;

    public void Start() {
        defaultScale = powerupPlayerTransform.localScale;
        _originalMass = powerupPlayerTransform.GetComponent<Rigidbody>().mass;

        string playerNumberString = powerupPlayerTransform.gameObject.tag.Substring(gameObject.tag.Length - 1);
        int playerNumber = int.Parse(playerNumberString);
        _assignedPlayerTag = (EnumPlayerTag)playerNumber;

        this.playerReferences = this.GetComponentInChildren<PlayerReferences>();
        if (this.playerReferences == null)
            this.playerReferences = this.powerupPlayerTransform.gameObject.GetComponent<PlayerReferences>();
    }

    public void GivePlayerPower(EnumPowerUp power) {
        if (currentPower == EnumPowerUp.None) {
            currentPower = power;
            playerText.DisplayPower(currentPower);
            _isPlayerDead = false;
        }

    }

    public void OnUsePower(InputAction.CallbackContext context) {
        if (this._isPowerActivated) return; // Power is active
        if (context.phase == InputActionPhase.Performed) {
            playerText.DisableSprite();
            switch (currentPower) {
                case EnumPowerUp.None:
                    break;

                case EnumPowerUp.Shrink:
                    SoundEffectManager.Instance.PlaySoundFXClip(shrinkSFX, transform, 1f);
                    StartCoroutine(ShrinkPlayerCoroutine());
                    break;

                case EnumPowerUp.Grow:
                    SoundEffectManager.Instance.PlaySoundFXClip(growSFX, transform, 1f);
                    StartCoroutine(GrowPlayerCoroutine());
                    break;

                case EnumPowerUp.Bomb:
                    StartCoroutine(BombPlayersCoroutine());
                    break;

                case EnumPowerUp.SlowTime:
                    SlowTime();
                    break;

                case EnumPowerUp.GravityControl:
                    GravityControl();
                    break;

                case EnumPowerUp.Freeze:
                    SoundEffectManager.Instance.PlaySoundFXClip(freezeSFX, transform, 1f);
                    FreezePlayers();
                    break;

                case EnumPowerUp.MultiBall:
                    SoundEffectManager.Instance.PlaySoundFXClip(multiBallSFX, transform, 1f);
                    MultiBall();
                    break;
                case EnumPowerUp.Mine:
                    StartCoroutine(MineExplosionCoroutine());
                    break;
                case EnumPowerUp.Honey:
                    StartHoneyEffectCoroutine();
                    break;

            }
        }
    }

    public IEnumerator ShrinkPlayerCoroutine() {
        this._isPowerActivated = true;

        // Set the shrink properties
        this.playerReferences.rb.mass = shrinkMass;
        this.powerupPlayerTransform.localScale = new Vector3(shrinkScale, shrinkScale, shrinkScale);

        // Start the cooldown timer
        float elapsedTime = 0f;

        while (elapsedTime < _powerUpCooldown) {
            // Check if the player has died
            if (_isPlayerDead) {
                // Reset the player's size and mass
                ResetPlayerSizeAndMass();
                yield break; // Exit the coroutine early
            }

            elapsedTime += Time.deltaTime; // Increment elapsed time for fixed duration
            yield return null; // Wait until the next frame
        }

        // Reset the size and mass after the cooldown finishes if the player is alive
        ResetPlayerSizeAndMass();
        this.playerReferences.PlayerStats.PowerUpUsed(_point);
    }


    // Resets the player's size and mass
    private void ResetPlayerSizeAndMass() {
        this.playerReferences.rb.mass = _originalMass;
        this.powerupPlayerTransform.localScale = defaultScale;
        this._isPowerActivated = false;
        // Optionally reset current power state
        this.currentPower = EnumPowerUp.None;
    }

    public IEnumerator GrowPlayerCoroutine() {
        this._isPowerActivated = true;

        // Set the shrink properties
        this.powerupPlayerTransform.GetComponent<Rigidbody>().mass = growMass;
        this.powerupPlayerTransform.localScale = new Vector3(growScale, growScale, growScale);

        // Start the cooldown timer
        float elapsedTime = 0f;

        while (elapsedTime < _powerUpCooldown) {
            // Check if the player has died
            if (_isPlayerDead) {
                // Reset the player's size and mass
                ResetPlayerSizeAndMass();
                yield break; // Exit the coroutine early
            }

            elapsedTime += Time.deltaTime; // Increment elapsed time for fixed duration
            yield return null; // Wait until the next frame
        }

        // Reset the size and mass after the cooldown finishes if the player is alive
        ResetPlayerSizeAndMass();
        this.playerReferences.PlayerStats.PowerUpUsed(_point);
    }

    public IEnumerator BombPlayersCoroutine() {
        this._isPowerActivated = true;
        //yield return new WaitForSeconds(3f);
        GameObject explosionGameObject = Instantiate(this.explosionEffect, _cameraTarget.transform.position, Quaternion.identity);
        explosionGameObject.GetComponent<ExplosionPowerUp>().AssignBombOwner(powerupPlayerTransform.gameObject);
        yield return new WaitForSeconds(_powerUpCooldown);
        this.playerReferences.PlayerStats.PowerUpUsed(_point);
        //gameObject.GetComponentInChildren<ModelController>().PlayerStats.PowerUpUsed(_point);
        this._isPowerActivated = false;
        this.currentPower = EnumPowerUp.None;
    }

    public IEnumerator MineExplosionCoroutine() {
        this._isPowerActivated = true;
        this.playerReferences.PlayerStats.PowerUpUsed(_point);
        //gameObject.GetComponentInChildren<ModelController>().PlayerStats.PowerUpUsed(_point);
        GameObject landMineGameObject = Instantiate(this.minePrefab, _cameraTarget.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_powerUpCooldown);
        this._isPowerActivated = false;
        this.currentPower = EnumPowerUp.None;

    }

    public void SlowTime() {
        Debug.Log("Work In Progress");
        Debug.Log("Slow Down Shlawg");
    }

    public IEnumerator GravityControl() {
        Debug.Log("Work In Progress");
        yield return new WaitForSeconds(_powerUpCooldown);
        Debug.Log("Gravity Flip");
    }
    private void FreezePlayers() {
        this._isPowerActivated = true;
        this.playerReferences.PlayerStats.PowerUpUsed(_point);
        //gameObject.GetComponentInChildren<ModelController>().PlayerStats.PowerUpUsed(_point);
        // Loop through all players in the GameManager
        foreach (var player in GameManager.Instance.Players) {
            // Check if the player is not Player01
            if (player.Key != _assignedPlayerTag) {
                // Set the player's rigidbody to kinematic to freeze them
                player.Value.GetComponentInChildren<PlayerReferences>().rb.isKinematic = true;
                // Start a coroutine to unfreeze the player after 5 seconds
                StartCoroutine(Unfreeze(player.Value));
            }
        }
        StartCoroutine(PowerCooldown());
    }
    public IEnumerator PowerCooldown() {
        yield return new WaitForSeconds(_powerUpCooldown);
        this._isPowerActivated = false;
        this.currentPower = EnumPowerUp.None;
    }

    // Coroutine to unfreeze the player after 5 seconds
    public IEnumerator Unfreeze(GameObject player) {
        yield return new WaitForSeconds(5f);
        // Set the player's rigidbody to non-kinematic to unfreeze them
        player.GetComponentInChildren<PlayerReferences>().rb.isKinematic = false;
    }

    private void StartHoneyEffectCoroutine() {
        // Start
        this._isPowerActivated = true;
        GameObject honeyGameObject = Instantiate(this.honeyPrefab, _cameraTarget.transform.position, Quaternion.identity);
        honeyGameObject.GetComponent<HoneyBlock>().AssignOwner(powerupPlayerTransform.gameObject);
        this.playerReferences.PlayerStats.PowerUpUsed(_point);
        StartCoroutine(PowerCooldown());
    }
    public void MultiBall() {
        this._isPowerActivated = true;
        this.playerReferences.PlayerStats.PowerUpUsed(_point);
        CreateDuplicate(powerupPlayerTransform.gameObject);
        CreateDuplicate(powerupPlayerTransform.gameObject);
        StartCoroutine(PowerCooldown());
    }

    private void CreateDuplicate(GameObject playerModel) {
        // Duplicate the object by instantiating it
        GameObject duplicate = Instantiate(playerModel.gameObject, powerupPlayerTransform.position, Quaternion.identity);
        StartCoroutine(DestroyAfterDelay(duplicate));
    }

    private IEnumerator DestroyAfterDelay(GameObject dub) {
        yield return new WaitForSecondsRealtime(_powerUpCooldown);
        // Destroy the GameObject
        Destroy(dub);
    }

    private IEnumerator BalloonCoroutine() {
        Instantiate(balloonPrefab);
        yield return new WaitForSecondsRealtime(_powerUpCooldown);
        balloonPrefab.SetActive(false);
        this.currentPower = EnumPowerUp.None;
    }

    public void RemoveCurrentPower() {

        playerText.DisableSprite();

        this.currentPower = EnumPowerUp.None; // Remove Holding Power Up
        _powerUpCooldown = 0f;
        _powerUpCooldown = 5f;
    }

    public void PlayerCollision(Collision player) {

        if (this.currentPower == EnumPowerUp.Grow) {
            if (player.gameObject.name.Contains("Clone")) {
                Destroy(player.gameObject);
                return;
            }
            if (player.gameObject.tag.StartsWith("Player")) {
                if (this.powerupPlayerTransform.localScale.x > player.gameObject.transform.localScale.x) {
                    EnumPlayerTag tag = player.gameObject.GetComponent<PlayerReferences>().GetPlayerTag();
                    PlayerJoinManager.Instance.Respawn(tag);
                    player.gameObject.GetComponent<PlayerReferences>().PlayerStats.PlayerDeaths(_point);
                    this.playerReferences.PlayerStats.PowerUpUsed(_point);
                }
            }

            if (player.gameObject.tag == ("Bumper")) {
                Destroy(player.gameObject);
            }
        }
    }
    // Call this method when the player dies
    public void OnPlayerDeath() {
        _isPlayerDead = true; // Set the flag to indicate player has died
    }

    public void PlayerRespawns() {
        OnPlayerDeath();
        this.currentPower = EnumPowerUp.None;
        this._isPowerActivated = false;
    }
}
