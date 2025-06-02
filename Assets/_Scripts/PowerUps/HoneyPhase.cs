using UnityEngine;

public class HoneyPhase : MonoBehaviour
{
    [SerializeField] private HoneyBlock _honeyBlock;
    public void OnTriggerEnter(Collider player) {
        if (player.gameObject.tag.StartsWith("Player")) {
            _honeyBlock.SlowPlayer(player);
        }
    }

    public void OnTriggerStay(Collider player) {
        if (player.gameObject.tag.StartsWith("Player")) {
            _honeyBlock.SlowPlayer(player);
        }
    }
}
