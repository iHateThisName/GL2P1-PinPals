using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
// Ivar
public class GameManager : Singleton<GameManager> {

    public Transform SpawnPoint;
    public bool IsPaused { get; private set; }
    // Prefabs
    [SerializeField] private GameObject PauseMenuPrefab;
    private GameObject _currentPauseMenu;
    public Dictionary<EnumPlayerTag, GameObject> Players {
        get { return Helper.Players; }
        private set { Helper.Players = value; }
    }

    public Dictionary<EnumPlayerTag, NavigationManager> PlayerNavigations = new Dictionary<EnumPlayerTag, NavigationManager>();
    [SerializeField] private CinemachineSplineDolly _dollyCart;
    private bool _isPlayersHidden = false;

    protected override void Awake() {
        base.Awake();
        IsPaused = false;
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        // Hiding the players when the game starts
        if (this._dollyCart != null) {
            HidePlayers();
        }
    }

    private IEnumerator Start() {
        string activeSceneName = SceneManager.GetActiveScene().name;
        if (this._dollyCart != null) {
            //Input Action
            InputAction cancel = InputSystem.actions.FindAction("Cancel");
            cancel.performed += cancelCallback;
            void cancelCallback(InputAction.CallbackContext ctx) {
                this._dollyCart.CameraPosition = 1f; // Skip to the end
            }

            TimeManager.instance.StopStopWatch(); // Stops the timer
            PlayerSettings.IsLandscape = false; // Set the camera to use a single camera
            CheckCamera(); // Updates the camera settings

            while (this._dollyCart.CameraPosition < 1f) { // Checks if the dolly cart has reached the end
                yield return new WaitForSeconds(0.1f);
            }

            // When the dolly cart has reached the end
            PlayerSettings.IsLandscape = true;
            CheckCamera();
            cancel.performed -= cancelCallback; // Stop listening to the cancel action
            StartGame();
            Debug.Log("Dolly cart has reach end");
            PlungerToolTipFirstTimer();
        } else if (activeSceneName == Helper.endGame || activeSceneName == Helper.mainMenu) {
            CheckCamera();
        } else {
            CheckCamera();
            StartGame();
        }
    }

    private void PlungerToolTipFirstTimer() {
        foreach (var player in Players)
        {
            PlayerTipUI playerTipUI = GetPlayerReferences(player.Key).playerTipUI;
            playerTipUI.IsFirstTime = true;
            playerTipUI.PlungerCollider();
        }
    }

    public void HidePlayers() {
        foreach (var player in Players) {
            PlayerReferences modelController = GetPlayerReferences(player.Key);
            modelController.GetPlayerMeshRenderer().enabled = false;
            modelController.PlayerFollowCanvasManager.gameObject.SetActive(false);
            modelController.PinballCamera.gameObject.SetActive(false);
        }
        this._isPlayersHidden = true;
    }

    private void ShowPlayers() {
        foreach (var player in Players) {
            PlayerReferences modelController = GetPlayerReferences(player.Key);
            modelController.GetPlayerMeshRenderer().enabled = true;
            modelController.PlayerFollowCanvasManager.gameObject.SetActive(true);
            modelController.PinballCamera.gameObject.SetActive(true);
        }
        this._isPlayersHidden = false;
    }

    public void StartGame() {
        // Show the players
        if (this._isPlayersHidden)
            ShowPlayers();

        // Start the timer
        TimeManager.instance.StartStopWatch();
    }
    private void PauseGame() {
        this.IsPaused = true;
        Time.timeScale = 0;
    }
    public void ResumeGame() {
        IsPaused = false;
        Time.timeScale = 1;
    }

    public void ReloadGame() {
        ResumeGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void OnPauseAction() {
        if (this.IsPaused) {
            ResumeGame();
            Destroy(_currentPauseMenu);
        } else {
            PauseGame();
            _currentPauseMenu = Instantiate(PauseMenuPrefab);
        }
    }

    public void CheckCamera() {
        foreach (var player in Players) {
            Camera playerCamera = player.Value.GetComponentInChildren<PlayerReferences>().PinballCamera;
            if (PlayerSettings.IsLandscape) {
                playerCamera.gameObject.SetActive(true);
                //playerCamera.rect = new Rect((int)player.Key / Players.Count, 0, 1 / Players.Count, 1); // Not Tested
            } else {
                playerCamera.gameObject.SetActive(false);
            }
        }
    }

    public void LevelSelector() {
        SceneManager.LoadScene("LevelSelector");
    }

    public void SelectScene(string SceneName) {
        PlayerSettings.SelectedLevel = SceneName;
    }

    public void LoadeSelectedScene() {
        SceneManager.LoadScene(PlayerSettings.SelectedLevel);
    }
    public void MainMenu() {
        DeleteAllPlayers();
        Helper.GameReset();
        ResumeGame();
        PlayerSettings.IsLandscape = true;
        SceneManager.LoadScene("MainMenu");

    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void DeleteAllPlayers() {
        GameObject[] players = Helper.Players.Values.ToArray();
        // loop through the array and destroy all the gameobjects
        foreach (GameObject player in players) {
            Destroy(player.transform.parent.gameObject);
        }
        Helper.Players.Clear();
    }
    public void DeletePlayer(EnumPlayerTag tag) {
        GameObject player = Players[tag];
        Destroy(player.transform.parent.gameObject);
        Players.Remove(tag);
    }

    public PlayerController GetPlayerController(EnumPlayerTag tag) {
        return Players[tag].GetComponent<PlayerController>();
    }

    public PlayerReferences GetPlayerReferences(EnumPlayerTag tag) {
        return Players[tag].transform.GetChild(0).GetComponent<PlayerReferences>();
    }

    public void MovePlayer(EnumPlayerTag tag, Vector3 newPosition, bool reEnableGravity = true) {
        if (!Players.ContainsKey(tag)) return;
        GameObject player = Players[tag];
        player.GetComponent<PlayerController>().MovePlayer(newPosition, reEnableGravity);
    }

    public List<EnumPlayerTag> GetPlayersOrderByScore() => GetPlayersOrderByScoreWithScore().Select(x => x.tag).ToList();
    public List<(EnumPlayerTag tag, int score)> GetPlayersOrderByScoreWithScore() {
        List<(EnumPlayerTag tag, int score)> playersOrderByScore = new List<(EnumPlayerTag tag, int score)>();

        foreach (GameObject player in Players.Values) {
            // Gets the player score
            int playerScore = player.transform.GetChild(0).GetComponent<PlayerReferences>().PlayerScoreTracker.currentScore;
            // Gets the player tag that is assaigned to the player gameobject
            EnumPlayerTag playerTag = Helper.GetPlayerTagKey(Players, player);
            // Adds the player tag and score to the list
            playersOrderByScore.Add((tag: playerTag, score: playerScore));
        }
        // Orders the list by score
        return playersOrderByScore.OrderByDescending(x => x.score).ToList();
    }

    public void ScoreScene() => SceneManager.LoadScene("EndGame");
    public bool IsPlayerHoldingInteraction(EnumPlayerTag tag) {
        if (this._isPlayersHidden) return false;
        return this.Players[tag].GetComponent<PlayerController>().IsHoldingInteraction;
    }

    //Einar
    public void GameModeSelect() {
        SceneManager.LoadScene("GameModeSelect");
    }

    public void OnApplicationQuit() {
        {
            PlayerPrefs.Save(); // Save PlayerPrefs before quitting
#if UNITY_EDITOR
            // If you're using the Unity Editor
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#else
        // If you're running a standalone build of the game
        Application.Quit();
#endif
        }
    }

    public void ProtoTypeLVL() {
        SceneManager.LoadScene("Level 01, LasVegas");
    }

    public void EndOfGameScore() {
        SceneManager.LoadScene("EndOfGameScore");

    }

    public void LoadeLobbyScene() {
        SceneManager.LoadScene("Lobby");
    }

    public void SplitScreen() {
        PlayerSettings.IsLandscape = true;
        //SceneManager.LoadScene("Level 01, vegasTP");

    }

    public void SingleScreen() {
        PlayerSettings.IsLandscape = false;
        //SceneManager.LoadScene("Level 01, vegasTP");

    }

}
