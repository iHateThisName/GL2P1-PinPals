using UnityEngine;

public class ExplosionPowerUp : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("tagName"))
        {
            gameObject.GetComponent<PlayerPowerController>()._isPlayerDead = true;
            Debug.Log("The enemies are dead!");
        }
    }

}
