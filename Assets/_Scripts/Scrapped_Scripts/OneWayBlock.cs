using UnityEngine;

public class OneWayBlock : MonoBehaviour
{
    private Collider playerCollider;
    [SerializeField] private Collider doorCollider;
    private void OnTriggerEnter(Collider player)
    {
        //if (player.gameObject.name.Contains("Player"))
        {
            // Prevent the player from entering the door
            Debug.Log("Player cannot enter from this side.");
            //playerCollider = other; // Store the reference to the player collider
            //Physics.IgnoreCollision(other, GetComponent<Collider>(), true); // temporarily ignore collision
            Physics.IgnoreCollision(player, doorCollider, false);
        }
    }
}
