using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowerController : MonoBehaviour {
    private EnumPowerUp currentPower = EnumPowerUp.None;
    [SerializeField] private Transform playerTransform;
    private Vector3 defaultScale;
    private float _powerUpCooldown = 5f;

    public void Start() {
        defaultScale = playerTransform.localScale;
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
        this.playerTransform.localScale = new Vector3(2f, 2f, 2f);
        await Task.Delay(3000);
        this.playerTransform.localScale = defaultScale;
        this.currentPower = EnumPowerUp.None;
    }

    public IEnumerator ShrinkPlayerCoroutine() {
        this.playerTransform.localScale = new Vector3(2f, 2f, 2f);
        yield return new WaitForSeconds(_powerUpCooldown);
        this.playerTransform.localScale = defaultScale;
        this.currentPower = EnumPowerUp.None;
    }
    public async void GrowPlayer() {
        this.playerTransform.localScale = new Vector3(10f, 10f, 10f);
        await Task.Delay(3000);
        this.playerTransform.localScale = defaultScale;
        this.currentPower = EnumPowerUp.None;
    }
    public IEnumerator GrowPlayerCoroutine() {
        this.playerTransform.localScale = new Vector3(10f, 10f, 10f);
        yield return new WaitForSeconds(_powerUpCooldown);
        this.playerTransform.localScale = defaultScale;
        this.currentPower = EnumPowerUp.None;
    }
    public void BombPlayers() {
        Debug.Log("Work In Progress");
        Debug.Log("Bomberman");
    }
    public void SlowTime() {
        Debug.Log("Work In Progress");
        Debug.Log("Slow Down Shlawg");
    }
    public void GravityControl() {
        Debug.Log("Work In Progress");
        Debug.Log("Gravity Flip");
    }
    public async void FreezePlayers() {
        this.GetComponentInParent<Rigidbody>().isKinematic = true;
        await Task.Delay(1000);
        this.currentPower = EnumPowerUp.None;
    }
    public IEnumerator FreezePlayersCoroutine() {
        this.GetComponentInParent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(_powerUpCooldown);
        this.currentPower = EnumPowerUp.None;
    }
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

    IEnumerator DestroyAfterDelay(GameObject dub) {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(_powerUpCooldown);
        // Destroy the GameObject
        Destroy(dub);
        Debug.Log("Destroying duplicate:" + dub.name);
    }
}
