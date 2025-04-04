using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
// Hilmir and Ivar
public class PlayerPowerController : MonoBehaviour {
    private EnumPowerUp currentPower = EnumPowerUp.None;
    [SerializeField] private Transform powerupPlayerTransform;
    [SerializeField] private Transform _cameraTarget;
    private Vector3 defaultScale;
    private float _powerUpCooldown = 5f;
    private float _originalMass = 0.02f;
    [SerializeField] private float shrinkMass = 0.01f;
    [SerializeField] private float growMass = 0.05f;
    [SerializeField] private float shrinkScale = 2f;
    [SerializeField] private float growScale = 10f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private GameObject balloonPrefab;
    [SerializeField] private GameObject honeyPrefab;
    //[SerializeField] public bool _isPlayerDead;
    //[SerializeField] public bool _isRespawned = false;

    [Header("Power up SFX")]
    [SerializeField] private AudioClip multiBallSFX;
    [SerializeField] private AudioClip freezeSFX;
    [SerializeField] private AudioClip growSFX;
    [SerializeField] private AudioClip shrinkSFX;
    [SerializeField] private AudioClip bombTickSFX;

    [SerializeField] private PlayerFollowCanvasManager playerText;

    // The tag assigned to the player, used to identify the player in the game.
    private EnumPlayerTag _assignedPlayerTag;
    //private Coroutine _currentPowerCoroutine = null;
    //private List<GameObject> _currentPowerUpCreation = new List<GameObject>();

    private bool _isPowerActivated = false;


    public void Start() {
        //bombTickAudioSource.Stop();
        defaultScale = powerupPlayerTransform.localScale;
        _originalMass = powerupPlayerTransform.GetComponent<Rigidbody>().mass;

        string playerNumberString = powerupPlayerTransform.gameObject.tag.Substring(gameObject.tag.Length - 1);
        int playerNumber = int.Parse(playerNumberString);
        _assignedPlayerTag = (EnumPlayerTag)playerNumber;
    }

    public void GivePlayerPower(EnumPowerUp power) {
        if (currentPower == EnumPowerUp.None) {
            currentPower = power;
            playerText.DisplayPower(currentPower);
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
                    /*                    this._currentPowerCoroutine = */
                    StartCoroutine(ShrinkPlayerCoroutine());
                    break;

                case EnumPowerUp.Grow:
                    SoundEffectManager.Instance.PlaySoundFXClip(growSFX, transform, 1f);
                    /*                    this._currentPowerCoroutine = */
                    StartCoroutine(GrowPlayerCoroutine());
                    break;

                case EnumPowerUp.Bomb:
                    /*                    this._currentPowerCoroutine =*/
                    StartCoroutine(BombPlayersCoroutine());
                    break;

                //case EnumPowerUp.Balloon:
                //    StartCoroutine(BalloonCoroutine());
                //    break;

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
                    /*                    this._currentPowerCoroutine =*/
                    StartCoroutine(StartHoneyEffectCoroutine());
                    break;

            }
        }
    }

    public async void ShrinkPlayer() {
        this.currentPower = EnumPowerUp.None;
        this.powerupPlayerTransform.localScale = new Vector3(shrinkScale, shrinkScale, shrinkScale);
        await Task.Delay(3000);
        this.powerupPlayerTransform.localScale = defaultScale;
    }

    public IEnumerator ShrinkPlayerCoroutine() { // Use Fixed Update to slowly shrink the ball, use Lerp.
        this._isPowerActivated = true;
        this.powerupPlayerTransform.GetComponent<Rigidbody>().mass = shrinkMass;
        this.powerupPlayerTransform.localScale = new Vector3(shrinkScale, shrinkScale, shrinkScale);
        //if (_isRespawned == true)
        //{
        //    this.playerTransform.GetComponent<Rigidbody>().mass = _originalMass;
        //    this.playerTransform.localScale = defaultScale;
        //    _isRespawned = false;
        //    yield break;
        //}
        //else
        //{
        yield return new WaitForSeconds(_powerUpCooldown);
        this.powerupPlayerTransform.GetComponent<Rigidbody>().mass = _originalMass;
        this.powerupPlayerTransform.localScale = defaultScale;
        this._isPowerActivated = false;
        this.currentPower = EnumPowerUp.None;

    }

    private void ResetPlayerState() {
        this.powerupPlayerTransform.GetComponent<Rigidbody>().mass = _originalMass;
        this.powerupPlayerTransform.localScale = defaultScale;


        //foreach (var item in _currentPowerUpCreation)
        //{
        //    Destroy(item);
        //}

        //_currentPowerUpCreation.Clear();


    }

    public IEnumerator GrowPlayerCoroutine() {
        this._isPowerActivated = true;
        this.powerupPlayerTransform.GetComponent<Rigidbody>().mass = growMass;
        this.powerupPlayerTransform.localScale = new Vector3(growScale, growScale, growScale);

        //if (_isRespawned == true)
        //{
        //    this.playerTransform.GetComponent<Rigidbody>().mass = _originalMass;
        //    this.playerTransform.localScale = defaultScale;
        //    _isRespawned = false;
        //    yield break;
        //}
        //else
        //{
        yield return new WaitForSeconds(_powerUpCooldown); // This will last for 3 seconds until you return back to normal
        this.powerupPlayerTransform.GetComponent<Rigidbody>().mass = _originalMass;
        this.powerupPlayerTransform.localScale = defaultScale;
        this._isPowerActivated = false;
        this.currentPower = EnumPowerUp.None;
        //}
    }

    public IEnumerator BombPlayersCoroutine() {
        this._isPowerActivated = true;
        //yield return new WaitForSeconds(3f);
        GameObject explosionGameObject = Instantiate(this.explosionEffect, _cameraTarget.transform.position, Quaternion.identity);
        explosionGameObject.GetComponent<ExplosionPowerUp>().AssignBombOwner(powerupPlayerTransform.gameObject);
        yield return new WaitForSeconds(_powerUpCooldown);
        this._isPowerActivated = false;
        this.currentPower = EnumPowerUp.None;
    }

    public IEnumerator MineExplosionCoroutine() {
        this._isPowerActivated = true;
        GameObject landMineGameObject = Instantiate(this.minePrefab, _cameraTarget.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_powerUpCooldown);
        this._isPowerActivated = false;
        this.currentPower = EnumPowerUp.None;

    }

    public void SlowTime() {
        Debug.Log("Work In Progress");
        Debug.Log("Slow Down Shlawg");
    }
    public async void GravityControl() {
        Debug.Log("Work In Progress");
        await Task.Delay(3000);
        Debug.Log("Gravity Flip");
    }

    public IEnumerator GravityControlCoroutine() {
        Debug.Log("Work In Progress");
        yield return new WaitForSeconds(_powerUpCooldown);
        Debug.Log("Gravity Flip");
    }
    private void FreezePlayers() {
        this._isPowerActivated = true;
        // Loop through all players in the GameManager
        foreach (var player in GameManager.Instance.Players) {
            // Check if the player is not Player01
            if (player.Key != _assignedPlayerTag) {
                // Set the player's rigidbody to kinematic to freeze them
                player.Value.GetComponentInChildren<ModelController>().rb.isKinematic = true;
                // Start a coroutine to unfreeze the player after 5 seconds
                StartCoroutine(Unfreeze(player.Value));
            }
        }
    }

    // Coroutine to unfreeze the player after 5 seconds
    public IEnumerator Unfreeze(GameObject player) {
        yield return new WaitForSeconds(5f);
        // Set the player's rigidbody to non-kinematic to unfreeze them
        player.GetComponentInChildren<ModelController>().rb.isKinematic = false;
        this._isPowerActivated = false;
        this.currentPower = EnumPowerUp.None;

    }
    //private IEnumerator FreezePlayersCoroutine() {
    //    this.GetComponentInParent<Rigidbody>().isKinematic = true;
    //    yield return new WaitForSeconds(_powerUpCooldown);
    //    this.currentPower = EnumPowerUp.None;
    //}


    private IEnumerator StartHoneyEffectCoroutine() {
        // Start
        this._isPowerActivated = true;
        GameObject honeyGameObject = Instantiate(this.honeyPrefab, _cameraTarget.transform.position, Quaternion.identity);
        honeyGameObject.GetComponent<HoneyBlock>().AssignOwner(powerupPlayerTransform.gameObject);
        yield return new WaitForSecondsRealtime(_powerUpCooldown);
        this._isPowerActivated = false;
        this.currentPower = EnumPowerUp.None;

    }
    public void MultiBall() {
        this._isPowerActivated = true;
        CreateDuplicate(powerupPlayerTransform.gameObject);
        CreateDuplicate(powerupPlayerTransform.gameObject);
    }

    private void CreateDuplicate(GameObject playerModel) {
        // Duplicate the object by instantiating it
        GameObject duplicate = Instantiate(playerModel.gameObject, powerupPlayerTransform.position, Quaternion.identity);
        //Set the real playerModel as the parent of the duplicates
        //duplicate.transform.SetParent(playerTransform);
        //this._currentPowerUpCreation.Add(duplicate);

        //duplicate.transform.position = playerTransform.position;  // Change position to (2, 0, 0)
        //duplicate.transform.rotation = Quaternion.identity;    // Reset rotation to default
        StartCoroutine(DestroyAfterDelay(duplicate));
    }

    private IEnumerator DestroyAfterDelay(GameObject dub) {
        // Wait for the specified amount of time
        //if (_isRespawned == true)
        //{
        //    Destroy(dub);
        //    _isRespawned = false;
        //    yield break;
        //}
        //else
        //{
        yield return new WaitForSecondsRealtime(_powerUpCooldown);
        // Destroy the GameObject
        Destroy(dub);
        this._isPowerActivated = false;
        this.currentPower = EnumPowerUp.None;

        //}
    }

    private IEnumerator BalloonCoroutine() {
        Instantiate(balloonPrefab);
        yield return new WaitForSecondsRealtime(_powerUpCooldown);
        balloonPrefab.SetActive(false);
        this.currentPower = EnumPowerUp.None;
    }

    public void RemoveCurrentPower() {

        //defaultScale = playerTransform.localScale;
        //_originalMass = playerTransform.GetComponent<Rigidbody>().mass;

        playerText.DisableSprite();


        this.currentPower = EnumPowerUp.None; // Remove Holding Power Up
        _powerUpCooldown = 0f;
        _powerUpCooldown = 5f;
        //if (this._currentPowerCoroutine != null)
        //{
        //    StopCoroutine(this._currentPowerCoroutine);
        //}

        //switch (this.currentPower)
        //{
        //    case EnumPowerUp.None:
        //        break;
        //    case EnumPowerUp.Grow:
        //        this.playerTransform.GetComponent<Rigidbody>().mass = _originalMass;
        //        this.playerTransform.localScale = defaultScale;
        //        break;
        //    case EnumPowerUp.Shrink:
        //        this.playerTransform.GetComponent<Rigidbody>().mass = _originalMass;
        //        this.playerTransform.localScale = defaultScale;
        //        break;
        //}

        //StartCoroutine(ResetCooldownAfterRespawn());
    }

    private IEnumerator ResetCooldownAfterRespawn() {
        // Wait for 5 seconds before setting cooldown to 5f
        yield return new WaitForSeconds(1f);

        // Reset the cooldown to 5 seconds
        _powerUpCooldown = 5f;
        Debug.Log("Power-up cooldown reset to: " + _powerUpCooldown);
    }

    public void PlayerCollision(Collision player) {

        if (this.currentPower == EnumPowerUp.Grow) {
            if (player.gameObject.name.Contains("Clone")) {
                Destroy(player.gameObject);
                return;
            }
            //if (player.gameObject != this.gameObject) // They can't be killed, and just holding it kills people, and if both of them have the power up and have it activated, they can't kill each other
            //{
            if (player.gameObject.tag.StartsWith("Player")) {
                if (this.powerupPlayerTransform.localScale.x > player.gameObject.transform.localScale.x) {
                    EnumPlayerTag tag = player.gameObject.GetComponent<ModelController>().GetPlayerTag();
                    PlayerJoinManager.Instance.Respawn(tag);
                }

                /*else if (this.playerTransform.localScale.x < player.gameObject.transform.localScale.x)
                {
                    EnumPlayerTag tag = player.gameObject.GetComponent<ModelController>().GetPlayerTag();
                    GameManager.Instance.GetPlayerController(tag).Respawn();
                }*/
            }
            //}

            if (player.gameObject.tag == ("Bumper")) {
                Destroy(player.gameObject);
            }
        }
    }

    //private IEnumerator EnablePowerUps()
    //{
    //    yield return new WaitForSecondsRealtime(1f);
    //    _isRespawned = false;
    //}
}
