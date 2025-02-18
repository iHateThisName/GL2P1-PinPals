using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
// Ivar
public class GameManager : Singleton<GameManager> {

    public bool IsPaused { get; private set; }

    public Dictionary<EnumPlayerTag, GameObject> Players { get; private set; } = new Dictionary<EnumPlayerTag, GameObject>();


    protected override void Awake() {
        base.Awake();
        IsPaused = false;
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }
    public void PauseGame() {
        IsPaused = true;
        Time.timeScale = 0;
    }
    public void ResumeGame() {
        IsPaused = false;
        Time.timeScale = 1;
    }

    public void ReloadGame() {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void OnPlayerJoined(PlayerInput playerInput) {
        // Create a enum that represent the player tag
        EnumPlayerTag playerTag = (EnumPlayerTag)playerInput.playerIndex + 1;

        // Adds the player to the dictonary also checks if player already exists in the dictionary.
        if (!Players.ContainsKey(playerTag)) {
            Players.Add(playerTag, playerInput.gameObject);
        } else if (Players.ContainsKey(playerTag) && Players[playerTag].gameObject != playerInput.gameObject) {
            playerTag = Helper.GetUnusedPlayerTag(Players, (int)playerTag);
            Players[playerTag] = playerInput.gameObject;
        }

        if (!PlayerSettings.IsLandscape) {
            //playerInput.gameObject.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1, 1);
            Camera playerCamera = playerInput.gameObject.GetComponentInChildren<ModelController>().PinballCamera;
            playerCamera.gameObject.SetActive(false);
        }
    }

    public void OnPlayerLeaves(PlayerInput playerInput) {
        EnumPlayerTag enumPlayerTag = Helper.GetPlayerTagKey(this.Players, playerInput.gameObject);
        Players.Remove(enumPlayerTag);
    }

    //Einar
    public void EndOfGameScore() {
        SceneManager.LoadScene("EndOfGameScore");

    }

    public void SplitScreen() {
        SceneManager.LoadScene("Prototype");

    }

    public void SingleScreen() {
        PlayerSettings.IsLandscape = false;
        SceneManager.LoadScene("Prototype");

    }
}
