using UnityEngine;

public class OneWayDoor : MonoBehaviour {
    private Collider playerCollider;
    [SerializeField] private Collider doorCollider;

    private void OnTriggerEnter(Collider player) {
        // Check if the object that entered the trigger is the player
        //if (player.gameObject.name.Contains("Player"))
        {

            // Allow the player to pass through the door
            Debug.Log("Player can enter the door.");
            Physics.IgnoreCollision(player, doorCollider, true);
            //playerCollider = null; // Clear the stored reference if the player enters correctly
        }
    }
}
