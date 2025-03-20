using UnityEngine;

public class MinefieldPowerUp : MonoBehaviour
{
    [SerializeField]
    private PlayerJoinManager respawnManager;
    private bool _isDangerous = false;
    void Start()
    {
        GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision other)
    {
        if (this._isDangerous) return;

        if (other.gameObject.tag.StartsWith("Player"))
        {
            EnumPlayerTag tag = other.gameObject.GetComponent<ModelController>().GetPlayerTag();
            respawnManager.Respawn(tag);
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


}
