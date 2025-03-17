using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

    protected override void Awake() {
        base.Awake();
        IsPaused = false;
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
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
            Camera playerCamera = player.Value.GetComponentInChildren<ModelController>().PinballCamera;
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
        SceneManager.LoadScene("StartScreen");
    }

    public void DeleteAllPlayers() {
        GameObject[] players = Helper.Players.Values.ToArray();
        // loop through the array and destroy all the gameobjects
        foreach (GameObject player in players) {
            Destroy(player.transform.parent.gameObject);
        }
    }
    public void DeletePlayer(EnumPlayerTag tag) {
        GameObject player = Players[tag];
        Destroy(player.transform.parent.gameObject);
        Players.Remove(tag);
    }

    public PlayerController GetPlayerController(EnumPlayerTag tag) {
        return Players[tag].GetComponent<PlayerController>();
    }

    public ModelController GetModelController(EnumPlayerTag tag) {
        return Players[tag].transform.GetChild(0).GetComponent<ModelController>();
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
            int playerScore = player.transform.GetChild(0).GetComponent<ModelController>().PlayerScoreTracker.currentScore;
            // Gets the player tag that is assaigned to the player gameobject
            EnumPlayerTag playerTag = Helper.GetPlayerTagKey(Players, player);
            // Adds the player tag and score to the list
            playersOrderByScore.Add((tag: playerTag, score: playerScore));
        }
        // Orders the list by score
        return playersOrderByScore.OrderByDescending(x => x.score).ToList();
    }

    public void ScoreScene() => SceneManager.LoadScene("EndGame");
    public bool IsPlayerHoldingInteraction(EnumPlayerTag tag) => this.Players[tag].GetComponent<PlayerController>().IsHoldingInteraction;

    //Einar
    public void GameModeSelect() {
        SceneManager.LoadScene("GameModeSelect");
    }

    public void ProtoTypeLVL()
    {
        SceneManager.LoadScene("Level 01, LasVegas");
    }

    public void EndOfGameScore() {
        SceneManager.LoadScene("EndOfGameScore");

    }

    public void LoadeLobbyScene()
    {
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
