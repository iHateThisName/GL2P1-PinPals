using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using System.Collections;

public class PlayerPowerController : MonoBehaviour
{
    private EnumPowerUp currentPower = EnumPowerUp.None;
    [SerializeField] private Transform playerTransform;
    private Vector3 defaultScale;
    private float _powerUpCooldown = 5f;

    public void Start()
    {
        defaultScale = playerTransform.localScale;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnUsePower();
        }
    }
    public void GivePlayerPower(EnumPowerUp power)
    {
        if (currentPower == EnumPowerUp.None)
        {
            currentPower = power;
        }

    }

    public void OnUsePower()
    {
        switch (currentPower)
        {
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

        }
    }

    public async void ShrinkPlayer()
    {
        this.playerTransform.localScale = new Vector3(2f, 2f, 2f);
        await Task.Delay(3000);
        this.playerTransform.localScale = defaultScale;
        this.currentPower = EnumPowerUp.None;
    }

    public IEnumerator ShrinkPlayerCoroutine()
    {
        this.playerTransform.localScale = new Vector3(2f, 2f, 2f);
        yield return new WaitForSeconds(_powerUpCooldown);
        this.playerTransform.localScale = defaultScale;
        this.currentPower = EnumPowerUp.None;
    }
    public async void GrowPlayer()
    {
        this.playerTransform.localScale = new Vector3(10f, 10f, 10f);
        await Task.Delay(3000);
        this.playerTransform.localScale = defaultScale;
        this.currentPower = EnumPowerUp.None;
    }
    public IEnumerator GrowPlayerCoroutine()
    {
        this.playerTransform.localScale = new Vector3(10f, 10f, 10f);
        yield return new WaitForSeconds(_powerUpCooldown);
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
    public void GravityControl()
    {
        Debug.Log("Work In Progress");
        Debug.Log("Gravity Flip");
    }
    public async void FreezePlayers()
    {
        this.GetComponentInParent<Rigidbody>().isKinematic = true;
        await Task.Delay(1000);
        this.currentPower = EnumPowerUp.None;
    }
}
