using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// Ivar
public class PlayerManager : MonoBehaviour {

    [SerializeField] private Transform _spawnPosition;
    public Dictionary<EnumPlayerTag, GameObject> Players {
        get { return Helper.Players; }
        private set { Helper.Players = value; }
    }

    public void OnPlayerJoined(PlayerInput playerInput) {
        PlayerController playerController = playerInput.gameObject.GetComponent<PlayerController>();
        playerController.DisableGravity();
        playerInput.gameObject.transform.parent.transform.position = _spawnPosition.position;

        // Create a enum that represent the player tag
        EnumPlayerTag playerTag = (EnumPlayerTag)playerInput.playerIndex + 1; // PlayerIndex starts at 0

        // Adds the player to the dictonary also checks if player already exists in the dictionary.
        if (!Players.ContainsKey(playerTag)) {
            Players.Add(playerTag, playerInput.gameObject);
        } else if (Players.ContainsKey(playerTag) && Players[playerTag].gameObject != playerInput.gameObject) {
            playerTag = Helper.GetUnusedPlayerTag(Players, (int)playerTag);
            Players[playerTag] = playerInput.gameObject;
        }

        // Tag the player with the player tag
        playerInput.gameObject.transform.GetChild(0).tag = playerTag.ToString();
        Debug.Log(playerTag.ToString() + " joined the game");

        // Layer the player to the correct player layer
        playerInput.gameObject.layer = LayerMask.NameToLayer("Player" + (int)playerTag);
        foreach (Transform child in playerInput.gameObject.transform) {
            child.gameObject.layer = LayerMask.NameToLayer("Player" + (int)playerTag);
        }

        // Enable gravity after 1 second
        //ReEnableGravity(playerController, 1);
        StartCoroutine(ReEnableGravityCoroutine(playerController, 0.5f));

        // The player should not be destroyed when a new scene is loaded, to keep the player controller scheme
        DontDestroyOnLoad(playerInput.gameObject.transform.parent.gameObject);
        playerInput.gameObject.transform.parent.gameObject.name = playerTag.ToString();

        Camera playerCamera = playerInput.camera;

        // Get the default culling mask of the camera
        int defaultCullingMask = playerCamera.cullingMask;

        // Combine the default culling mask with the current layer + 4(Number of max players) to get to the flipper layers
        int currentLayer = 5 + (int)playerTag;
        playerCamera.cullingMask = defaultCullingMask | 1 << currentLayer + 4;  // Defaultmasks + flippers

        GameManager.Instance.CheckCamera();
    }

    public void OnPlayerLeaves(PlayerInput playerInput) {
        EnumPlayerTag playerTag = (EnumPlayerTag)playerInput.playerIndex + 1; // PlayerIndex starts at 0
        Players.Remove(playerTag);
        Helper.Players.Remove(playerTag);
        Debug.Log(playerTag.ToString() + " left the game");
    }

    private IEnumerator ReEnableGravityCoroutine(PlayerController playerController, float delayInSeconds) {
        yield return new WaitForSeconds(delayInSeconds);
        playerController.EnableGravity();
    }

}
