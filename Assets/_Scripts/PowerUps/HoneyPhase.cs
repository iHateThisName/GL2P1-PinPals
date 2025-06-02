using UnityEngine;

public class HoneyPhase : MonoBehaviour
{
    [SerializeField] private HoneyBlock _honeyBlock;
    [SerializeField] private float _waitTime;
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
    public void OnTriggerExit(Collider player) {
        //Debug.Log("Honey Off" + player.gameObject.name);
        PlayerReferences playerRef = player.gameObject.GetComponent<PlayerReferences>();
        playerRef.rb.linearDamping = 0;
        StartCoroutine(_honeyBlock.DestroyGameObject(_waitTime));
    }
}
