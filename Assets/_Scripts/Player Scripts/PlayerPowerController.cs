using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
// Hilmir and Ivar
public class PlayerPowerController : MonoBehaviour {
    private EnumPowerUp currentPower = EnumPowerUp.None;
    [SerializeField] private Transform playerTransform;
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
    [SerializeField] public bool _isPlayerDead;
    [SerializeField] public bool _isRespawned = false;

    [Header("Power up SFX")]
    [SerializeField] private AudioClip multiBallSFX;
    [SerializeField] private AudioClip freezeSFX;
    [SerializeField] private AudioClip growSFX;
    [SerializeField] private AudioClip shrinkSFX;
    [SerializeField] private AudioClip bombTickSFX;

    [SerializeField] private PlayerFollowCanvasManager playerText;
    // The tag assigned to the player, used to identify the player in the game.
    private EnumPlayerTag _assignedPlayerTag;

    public void Start() {
        //bombTickAudioSource.Stop();
        defaultScale = playerTransform.localScale;
        _originalMass = playerTransform.GetComponent<Rigidbody>().mass;

        string playerNumberString = playerTransform.gameObject.tag.Substring(gameObject.tag.Length - 1);
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
                    MineExplosionCoroutine();
                    break;

            }
        }
    }

    public async void ShrinkPlayer() {
        this.playerTransform.localScale = new Vector3(shrinkScale, shrinkScale, shrinkScale);
        await Task.Delay(3000);
        this.playerTransform.localScale = defaultScale;
        this.currentPower = EnumPowerUp.None;
    }

    public IEnumerator ShrinkPlayerCoroutine() { // Use Fixed Update to slowly shrink the ball, use Lerp.
        this.currentPower = EnumPowerUp.None;
        this.playerTransform.GetComponent<Rigidbody>().mass = shrinkMass;
        this.playerTransform.localScale = new Vector3(shrinkScale, shrinkScale, shrinkScale);
        if (_isRespawned == true)
        {
            this.playerTransform.GetComponent<Rigidbody>().mass = _originalMass;
            this.playerTransform.localScale = defaultScale;
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(_powerUpCooldown);
            this.playerTransform.GetComponent<Rigidbody>().mass = _originalMass;
            this.playerTransform.localScale = defaultScale;
        }
    }
    public async void GrowPlayer() {
        this.playerTransform.GetComponent<Rigidbody>().mass = growMass;
        this.playerTransform.localScale = new Vector3(growScale, growScale, growScale);
        await Task.Delay(3000);
        this.playerTransform.GetComponent<Rigidbody>().mass = _originalMass;
        this.playerTransform.localScale = defaultScale;
        this.currentPower = EnumPowerUp.None;
    }
    public IEnumerator GrowPlayerCoroutine() {
        this.currentPower = EnumPowerUp.None;
        this.playerTransform.GetComponent<Rigidbody>().mass = growMass;
        this.playerTransform.localScale = new Vector3(growScale, growScale, growScale);
        if (_isRespawned == true)
        {
            this.playerTransform.GetComponent<Rigidbody>().mass = _originalMass;
            this.playerTransform.localScale = defaultScale;
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(_powerUpCooldown); // This will last for 3 seconds until you return back to normal
            this.playerTransform.GetComponent<Rigidbody>().mass = _originalMass;
            this.playerTransform.localScale = defaultScale;
        }
    }

    public IEnumerator BombPlayersCoroutine() {
        
        //yield return new WaitForSeconds(3f);
        GameObject explosionGameObject = Instantiate(this.explosionEffect, _cameraTarget.transform.position, Quaternion.identity);
        explosionGameObject.GetComponent<ExplosionPowerUp>().AssignBombOwner(playerTransform.gameObject);
        this.currentPower = EnumPowerUp.None;
        yield return new WaitForSeconds(_powerUpCooldown);
    }
    public IEnumerator MineExplosionCoroutine()
    {
        GameObject landMineGameObject = Instantiate(this.minePrefab, _cameraTarget.transform.position, Quaternion.identity);
        this.currentPower = EnumPowerUp.None;
        yield return new WaitForSeconds(_powerUpCooldown);
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
        this.currentPower = EnumPowerUp.None;
    }

    // Coroutine to unfreeze the player after 5 seconds
    public IEnumerator Unfreeze(GameObject player) {
        if (_isRespawned == true)
        {
            player.GetComponentInChildren<ModelController>().rb.isKinematic = false;
        } else
        yield return new WaitForSeconds(5f);
        // Set the player's rigidbody to non-kinematic to unfreeze them
        player.GetComponentInChildren<ModelController>().rb.isKinematic = false;
    }
    //private IEnumerator FreezePlayersCoroutine() {
    //    this.GetComponentInParent<Rigidbody>().isKinematic = true;
    //    yield return new WaitForSeconds(_powerUpCooldown);
    //    this.currentPower = EnumPowerUp.None;
    //}
    public void MultiBall() {
        CreateDuplicate(playerTransform.gameObject);
        CreateDuplicate(playerTransform.gameObject);
        this.currentPower = EnumPowerUp.None;
    }

    private void CreateDuplicate(GameObject playerModel) {
        // Duplicate the object by instantiating it
        GameObject duplicate = Instantiate(playerModel.gameObject, playerTransform.position, Quaternion.identity);

        //duplicate.transform.position = playerTransform.position;  // Change position to (2, 0, 0)
        //duplicate.transform.rotation = Quaternion.identity;    // Reset rotation to default
        StartCoroutine(DestroyAfterDelay(duplicate));
    }

    private IEnumerator DestroyAfterDelay(GameObject dub) {
        // Wait for the specified amount of time
        if (_isRespawned == true)
        {
            Destroy(dub);
            yield break;
        }
        else
        {
            yield return new WaitForSecondsRealtime(_powerUpCooldown);
            // Destroy the GameObject
            Destroy(dub);
        }
    }

    private IEnumerator BalloonCoroutine() {
        Instantiate(balloonPrefab);
        yield return new WaitForSecondsRealtime(_powerUpCooldown);
        balloonPrefab.SetActive(false);
        this.currentPower = EnumPowerUp.None;
    }

    public void RemoveCurrentPower()
    {
        _isRespawned = true;
        this.currentPower = EnumPowerUp.None; // Remove Holding Power Up
        _isRespawned = false;
    }
}
