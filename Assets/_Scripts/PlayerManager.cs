using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
// Ivar
public class PlayerManager : MonoBehaviour {

    [SerializeField] private Transform _spawnPosition;
    public void OnPlayerJoined(PlayerInput playerInput) {
        PlayerController playerController = playerInput.gameObject.GetComponent<PlayerController>();
        playerController.DisableGravity();
        playerInput.gameObject.transform.parent.transform.position = _spawnPosition.position;

        // Enable gravity after 1 second
        StartCoroutine(ReEnableGravity(playerController, 1f));
    }

    private IEnumerator ReEnableGravity(PlayerController playerController, float delay) {
        yield return new WaitForSeconds(delay);
        playerController.EnableGravity();
    }
}
