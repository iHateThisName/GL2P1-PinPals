using UnityEngine;

public class PlayerCollisionController : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private PlayerPowerController powerController;
    [SerializeField] private PlayerReferences playerReference;
    [SerializeField] private PlayerTipUI playerTipUI;

    private void Start() {
        powerController = playerReference.PlayerPowerController;
    }

    private void OnCollisionEnter(Collision collision) {
        powerController.PlayerCollision(collision);
        //playerReference.PlayerStats.PlayerKills(1);
        //if (collision.gameObject.CompareTag("Plunger")) {
        //    playerTipUI.PlungerCollider();
        //}
        if (!collision.gameObject.CompareTag("Untagged") && collision.gameObject.CompareTag("Plunger")) {
            playerTipUI.PlungerCollider();
        }
    }

    private void OnCollisionExit(Collision collision) {

        playerTipUI.PlungerColliderExit();
    }
}
