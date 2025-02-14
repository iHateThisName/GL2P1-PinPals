using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowerController : MonoBehaviour {
    private EnumPowerUp currentPower = EnumPowerUp.None;
    [SerializeField] private Transform playerTransform;
    private Vector3 defaultScale;
    private float _powerUpCooldown = 5f;
    private float originalMass = 0.02f;
    [SerializeField] private float shrinkMass = 0.01f;
    [SerializeField] private float growMass = 0.05f;
    [SerializeField] private float shrinkScale = 2f;
    [SerializeField] private float growScale = 10f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject balloonPrefab;

    public void Start() {
        defaultScale = playerTransform.localScale;
        originalMass = playerTransform.GetComponent<Rigidbody>().mass;
    }

    public void GivePlayerPower(EnumPowerUp power) {
        if (currentPower == EnumPowerUp.None) {
            currentPower = power;
        }

    }

    public void OnUsePower(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            switch (currentPower) {
                case EnumPowerUp.None:
                    break;

                case EnumPowerUp.Shrink:
                    StartCoroutine(ShrinkPlayerCoroutine());
                    break;

                case EnumPowerUp.Grow:
                    StartCoroutine(GrowPlayerCoroutine());
                    break;

                case EnumPowerUp.Bomb:
                    BombPlayers();
                    break;

                case EnumPowerUp.SlowTime:
                    SlowTime();
                    break;

                case EnumPowerUp.GravityControl:
                    GravityControl();
                    break;

                case EnumPowerUp.Freeze:
                    FreezePlayers();
                    break;

                case EnumPowerUp.MultiBall:
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
        this.playerTransform.GetComponent<Rigidbody>().mass = originalMass;
        this.playerTransform.localScale = defaultScale;
        this.currentPower = EnumPowerUp.None;
    }
    public async void GrowPlayer() {
        this.playerTransform.GetComponent<Rigidbody>().mass = growMass;
        this.playerTransform.localScale = new Vector3(growScale, growScale, growScale);
        await Task.Delay(3000);
        this.playerTransform.GetComponent<Rigidbody>().mass = originalMass;
        this.playerTransform.localScale = defaultScale;
        this.currentPower = EnumPowerUp.None;
    }
    public IEnumerator GrowPlayerCoroutine() {
        this.playerTransform.GetComponent<Rigidbody>().mass = growMass;
        this.playerTransform.localScale = new Vector3(growScale, growScale, growScale);
        yield return new WaitForSeconds(_powerUpCooldown);
        this.playerTransform.GetComponent<Rigidbody>().mass = originalMass;
        this.playerTransform.localScale = defaultScale;
        this.currentPower = EnumPowerUp.None;
    }
    public void BombPlayers()
    {
        Debug.Log("Work In Progress");
        Debug.Log("Bomberman");
    }
    public void SlowTime()
    {
        Debug.Log("Work In Progress");
        Debug.Log("Slow Down Shlawg");
    }
    public async void GravityControl()
    {
        Debug.Log("Work In Progress");
        Debug.Log("Gravity Flip");
    }
    private async void FreezePlayers()
    {
        this.GetComponentInParent<Rigidbody>().isKinematic = true;
        await Task.Delay(1000);
        this.currentPower = EnumPowerUp.None;
    }
    private IEnumerator FreezePlayersCoroutine()
    {
        this.GetComponentInParent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(_powerUpCooldown);
        this.currentPower = EnumPowerUp.None;
    }
    private void MultiBall()
    {
        CreateDuplicate(playerTransform.gameObject);
        CreateDuplicate(playerTransform.gameObject);
        this.currentPower = EnumPowerUp.None;
    }

    private void CreateDuplicate(GameObject playerModel)
    {
        // Duplicate the object by instantiating it
        GameObject duplicate = Instantiate(playerModel.gameObject, playerTransform.position, Quaternion.identity);

        //duplicate.transform.position = playerTransform.position;  // Change position to (2, 0, 0)
        //duplicate.transform.rotation = Quaternion.identity;    // Reset rotation to default
        StartCoroutine(DestroyAfterDelay(duplicate));
    }

    private IEnumerator DestroyAfterDelay(GameObject dub)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(_powerUpCooldown);
        // Destroy the GameObject
        Destroy(dub);
        Debug.Log("Destroying duplicate:" + dub.name);
    }
}
