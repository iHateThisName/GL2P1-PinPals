using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowerController : MonoBehaviour {
    private EnumPowerUp currentPower = EnumPowerUp.None;
    [SerializeField] private Transform playerTransform;
    private Vector3 defaultScale;
    private float _powerUpCooldown = 5f;
    private float _originalMass = 0.02f;
    [SerializeField] private float shrinkMass = 0.01f;
    [SerializeField] private float growMass = 0.05f;
    [SerializeField] private float shrinkScale = 2f;
    [SerializeField] private float growScale = 10f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject balloonPrefab;
    [SerializeField] private AudioSource bombTickAudioSource;
    [SerializeField] public bool _isPlayerDead;
    [SerializeField] private AudioClip multiBallSFX;
    [SerializeField] private AudioClip freezeSFX;
    [SerializeField] private AudioClip growSFX;
    [SerializeField] private AudioClip shrinkSFX;

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
                    SoundEffectManager.instance.PlaySoundFXClip(shrinkSFX, transform, 1f);
                    StartCoroutine(ShrinkPlayerCoroutine());
                    break;

                case EnumPowerUp.Grow:
                    SoundEffectManager.instance.PlaySoundFXClip(growSFX, transform, 1f);
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
                    SoundEffectManager.instance.PlaySoundFXClip(freezeSFX, transform, 1f);
                    FreezePlayers();
                    break;

                case EnumPowerUp.MultiBall:
                    SoundEffectManager.instance.PlaySoundFXClip(multiBallSFX, transform, 1f);
                    MultiBall();
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
        this.playerTransform.GetComponent<Rigidbody>().mass = shrinkMass;
        this.playerTransform.localScale = new Vector3(shrinkScale, shrinkScale, shrinkScale);
        yield return new WaitForSeconds(_powerUpCooldown);
        this.playerTransform.GetComponent<Rigidbody>().mass = _originalMass;
        this.playerTransform.localScale = defaultScale;
        this.currentPower = EnumPowerUp.None;
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
        this.playerTransform.GetComponent<Rigidbody>().mass = growMass;
        this.playerTransform.localScale = new Vector3(growScale, growScale, growScale);
        yield return new WaitForSeconds(_powerUpCooldown); // This will last for 3 seconds until you return back to normal
        this.playerTransform.GetComponent<Rigidbody>().mass = _originalMass;
        this.playerTransform.localScale = defaultScale;
        this.currentPower = EnumPowerUp.None;
    }
    public async void BombPlayers() {
        foreach (var player in GameManager.Instance.Players)
        {
            bombTickAudioSource.Play();
            await Task.Delay(3000);
            bombTickAudioSource.Stop();
            if (player.Key != _assignedPlayerTag)
            {
                Instantiate(this.explosionEffect);
                await Task.Delay(2000);
            }
        }
    }
    public IEnumerator BombPlayersCoroutine() {
        foreach (var player in GameManager.Instance.Players)
        {
            bombTickAudioSource.Play();
            yield return new WaitForSeconds(3f);
            bombTickAudioSource.Stop();
            // Check if the player is not Player01
            if (player.Key != _assignedPlayerTag)
            {
                Instantiate(this.explosionEffect);
                yield return new WaitForSeconds(2f);
            }
        }
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
        yield return new WaitForSeconds(_powerUpCooldown);
        // Destroy the GameObject
        Destroy(dub);
    }

    private IEnumerator BalloonCoroutine() {
        Instantiate(balloonPrefab);
        yield return new WaitForSecondsRealtime(_powerUpCooldown);
        balloonPrefab.SetActive(false);
        this.currentPower = EnumPowerUp.None;
    }
}
