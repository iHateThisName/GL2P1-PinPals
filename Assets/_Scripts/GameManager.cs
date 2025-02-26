using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
// Ivar
public class GameManager : Singleton<GameManager> {

    public bool IsPaused { get; private set; }
    // Prefabs
    [SerializeField] private GameObject PauseMenuPrefab;
    private GameObject _currentPauseMenu;
    public Dictionary<EnumPlayerTag, GameObject> Players {
        get { return Helper.Players; }
        private set { Helper.Players = value; }
    }


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
    private void ResumeGame() {
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

    //Einar

    public void GameModeSelect() {
        SceneManager.LoadScene("GameModeSelect");
    }

    public void MainMenu() {
        //GameObject[] players = Players.Values.ToArray();
        //// loop through the array and destroy all the gameobjects
        //foreach (GameObject player in players) {
        //    Debug.Log(player.transform.parent.gameObject.name);
        //    Destroy(player.transform.parent.gameObject);
        //}
        //Players.Clear();

        GameObject[] players = Helper.Players.Values.ToArray();
        // loop through the array and destroy all the gameobjects
        foreach (GameObject player in players) {
            Destroy(player.transform.parent.gameObject);
        }

        Helper.GameReset();
        ResumeGame();
        SceneManager.LoadScene("StartScreen");
    }
    public void EndOfGameScore() {
        SceneManager.LoadScene("EndOfGameScore");

    }

    public void SplitScreen() {
        PlayerSettings.IsLandscape = true;
        SceneManager.LoadScene("Level 01, vegasTP");

    }

    public void SingleScreen() {
        PlayerSettings.IsLandscape = false;
        SceneManager.LoadScene("Level 01, vegasTP");

    }
}
