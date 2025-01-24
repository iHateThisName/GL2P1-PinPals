using UnityEngine;
// Ivar
public class GameManager : Singleton<GameManager> {

    public bool IsPaused { get; private set; }

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
}
