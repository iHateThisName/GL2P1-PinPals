using UnityEngine;
using UnityEngine.SceneManagement;
// Ivar
public class GameManager : Singleton<GameManager> {

    public bool IsPaused { get; private set; }


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
    //Einar
    public void EndOfGameScore() {
        SceneManager.LoadScene("EndOfGameScore");

    }
}
