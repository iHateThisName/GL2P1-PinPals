using System.Linq;
using UnityEngine;
// Einar
public class LayerZoneController : MonoBehaviour {
    private void OnTriggerEnter(Collider player) {
        EnumPlayerTag tag = player.gameObject.GetComponent<PlayerReferences>().GetPlayerTag();
        switch (tag) {
            case EnumPlayerTag.Player01:
                HandlePlayerCollision(player: player, playerTag: EnumPlayerTag.Player01, ignoreCollision: true);
                break;
            case EnumPlayerTag.Player02:
                HandlePlayerCollision(player: player, playerTag: EnumPlayerTag.Player02, ignoreCollision: true);
                break;
            case EnumPlayerTag.Player03:
                HandlePlayerCollision(player: player, playerTag: EnumPlayerTag.Player03, ignoreCollision: true);
                break;
            case EnumPlayerTag.Player04:
                HandlePlayerCollision(player: player, playerTag: EnumPlayerTag.Player04, ignoreCollision: true);
                break;
            default:
                Debug.Log("Unknown object entered the trigger zone");
                break;
        }
    }

    private void HandlePlayerCollision(Collider player, EnumPlayerTag playerTag, bool ignoreCollision) {
        Debug.Log(playerTag.ToString() + " entered/exit the trigger zone!");
        EnumPlayerTag[] allLiveTags = GameManager.Instance.Players.Keys.ToArray();

        foreach (EnumPlayerTag otherTag in allLiveTags) {
            // Skip the player's own tag
            if (otherTag == playerTag) continue;

            // Find the other player's collider in the scene
            Collider otherCollider = GameManager.Instance.GetPlayerReferences(otherTag).GetPlayerCollider();

            if (otherCollider != null) {
                // Ignore collision between the current player and the other player
                Physics.IgnoreCollision(player, otherCollider, ignoreCollision);
            }
        }
    }

    private void OnTriggerExit(Collider player) {
        EnumPlayerTag tag = player.gameObject.GetComponent<PlayerReferences>().GetPlayerTag();
        switch (tag) {
            case EnumPlayerTag.Player01:
                HandlePlayerCollision(player: player, playerTag: EnumPlayerTag.Player01, ignoreCollision: false);
                break;
            case EnumPlayerTag.Player02:
                HandlePlayerCollision(player: player, playerTag: EnumPlayerTag.Player02, ignoreCollision: false);
                break;
            case EnumPlayerTag.Player03:
                HandlePlayerCollision(player: player, playerTag: EnumPlayerTag.Player03, ignoreCollision: false);
                break;
            case EnumPlayerTag.Player04:
                HandlePlayerCollision(player: player, playerTag: EnumPlayerTag.Player04, ignoreCollision: false);
                break;
            default:
                Debug.Log("Unknown object entered the trigger zone");
                break;
        }
    }


}
