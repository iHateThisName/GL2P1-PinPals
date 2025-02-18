using UnityEngine;

public class ExplosionPowerUp : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("tagName"))
        {
            gameObject.GetComponent<PlayerPowerController>()._isPlayerDead = true;
            collision.transform.position = spawnPoint.position;
            Debug.Log("The enemies are dead!");
        }
    }

}
